using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using InfoTrackSEO.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace InfoTrackSEO.Core.Scraping.Strategies;

public class PowerShellManualParseStrategy : IScrapingStrategyExecutor
{
    private const int PageLoadDelaySeconds = 3;
    private readonly ILogger<PowerShellManualParseStrategy> _logger;

    public PowerShellManualParseStrategy(ILogger<PowerShellManualParseStrategy> logger)
    {
        _logger = logger;
    }

    public async Task<string> ExecuteAsync(string searchUrl)
    {
        string powershellScript = BuildPowerShellScript(searchUrl);
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -Command \"{powershellScript}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false
            }
        };
        
        process.Start();
        var output = await process.StandardOutput.ReadToEndAsync();
        
        if (!process.WaitForExit((PageLoadDelaySeconds + 10) * 1000))
        {
            try { process.Kill(); } catch { }
            _logger.LogError("PowerShell script execution timed out for URL '{SearchUrl}'.", searchUrl);
            throw new TimeoutException("PowerShell script execution timed out.");
        }
        
        if (string.IsNullOrWhiteSpace(output))
        {
            _logger.LogError("No HTML was captured from the clipboard for URL '{SearchUrl}'.", searchUrl);
            throw new Exception("No HTML was captured from the clipboard.");
        }
        process.Close();
        process.Dispose();
        return output;
    }

    private string BuildPowerShellScript(string url)
    {
        string escapedUrl = url.Replace("'", "''");
        var scriptBuilder = new StringBuilder();
        scriptBuilder.AppendLine($"$process = Start-Process -FilePath '{escapedUrl}' -PassThru");
        scriptBuilder.AppendLine($"Start-Sleep -Seconds {PageLoadDelaySeconds}");
        scriptBuilder.AppendLine("Add-Type -AssemblyName Microsoft.VisualBasic");
        scriptBuilder.AppendLine("if ($process -ne $null -and $process.MainWindowHandle -ne [System.IntPtr]::Zero) {");
        scriptBuilder.AppendLine("    [Microsoft.VisualBasic.Interaction]::AppActivate($process.Id) | Out-Null");
        scriptBuilder.AppendLine("}");
        scriptBuilder.AppendLine("Add-Type -AssemblyName System.Windows.Forms");
        scriptBuilder.AppendLine("[System.Windows.Forms.SendKeys]::SendWait('^+j')");
        scriptBuilder.AppendLine("Start-Sleep -Seconds 2");
        scriptBuilder.AppendLine("[System.Windows.Forms.SendKeys]::SendWait(\"copy{(}document.documentElement.outerHTML{)}{ENTER}\")");
        scriptBuilder.AppendLine("Start-Sleep -Seconds 2");
        scriptBuilder.AppendLine("Add-Type -AssemblyName PresentationCore");
        scriptBuilder.AppendLine("$html = [Windows.Clipboard]::GetText()\nWrite-Output $html");
        return scriptBuilder.ToString().Replace("\"", "\\\"");
    }
}