using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using InfoTrackSEO.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Web;

namespace InfoTrackSEO.Core.Scraping.Parsers
{
    /// <summary>
    /// Implements IHtmlParser using regular expressions to find search result links
    /// and determine the rank of a target URL.
    /// </summary>
    public class ManualRegexParser : IHtmlParser
    {
        private readonly ILogger<ManualRegexParser> _logger;
        private static readonly Regex SearchResultLinkRegex = new Regex(
            @"<div class=""yuRUbf"">.*?<a\s+.*?href=""(?<hrefval>[^""]+)""[^>]*>\s*<h3.*?>.*?</h3>.*?</a>.*?</div>",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public ManualRegexParser(ILogger<ManualRegexParser> logger)
        {
            _logger = logger;
        }

        public List<int> FindUrlRanks(string htmlContent, string targetUrl)
        {
            var matches = GetMatches(htmlContent);
            return GetRanks(matches, targetUrl);
        }
        private MatchCollection GetMatches(string htmlContent)
        {
            try
            {
                return SearchResultLinkRegex.Matches(htmlContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Regex matching failed in ManualRegexParser.");
                throw new InvalidOperationException("Regex matching failed.", ex);
            }
        }

        private List<int> GetRanks(MatchCollection matches, string targetUrl)
        {
            var ranks = new List<int>();
            if (matches.Count == 0) return ranks;
            int rank = 1;
            foreach (Match match in matches)
            {
                if (rank > 100) break;
                var href = GetHrefValue(match);
                if (IsUrlMatch(href, targetUrl))
                {
                    ranks.Add(rank);
                }
                rank++;
            }
            return ranks;
        }

        private static string GetHrefValue(Match match)
        {
            return HttpUtility.HtmlDecode(match.Groups["hrefval"]?.Value ?? string.Empty);
        }

        private static bool IsUrlMatch(string href, string targetUrl)
        {
            return href.Contains(targetUrl, StringComparison.OrdinalIgnoreCase);
        }
    }
}


// Here are the most relevant snippets the HTML, illustrating these patterns: DONT REMOVE THIS COMMENT
// 1. Standard Search Result (yuRUbf with a direct link)
// This is the most common case your primary regex (SearchResultLinkRegex) will match in standard organic results.
// <!-- Relevant Part: Matches SearchResultLinkRegex. -->
// <!-- The href attribute value starts with "https://", so it's treated as a direct link. -->
// <div class="MjjYud">
//     <div class="vt6azd Ww4FFb" data-hveid="CBwQAA">
//         <h2 class="bNg8Rb">Web result with site links</h2>
//         <div class="BYM4Nd">
//             <div class="eKjLze">
//                 <div class="Y6JuXb">
//                     <div lang="en" data-hveid="CCEQAA" data-ved="2ahUKEwjLpvW6ze-MAxUbTqQEHVS8K_EQFSgAegQIIRAA">
//                         <!-- This is the start of the block matched by SearchResultLinkRegex -->
//                         <div style="position:relative" class="tF2Cxc">
//                             <div><div></div>
//                             <div>
//                                 <!-- The specific class the regex looks for -->
//                                 <div class="yuRUbf">
//                                     <div class="b8lM7">
//                                         <span jscontroller="msmzHf" jsaction="rcuQ6b:npT2md;PYDNKe:bLV6Bd;mLt3mc">
//                                             <!-- The first 'a' tag whose 'href' is captured -->
//                                             <a jsname="UWckNb" class="zReHs" href="https://www.gov.uk/" data-ved="2ahUKEwjLpvW6ze-MAxUbTqQEHVS8K_EQFnoECBsQAQ" ping="/url?sa=t&source=web&rct=j&opi=89978449&url=https://www.gov.uk/&ved=2ahUKEwjLpvW6ze-MAxUbTqQEHVS8K_EQFnoECBsQAQ">
//                                                 <h3 class="LC20lb MBeuO DKV0Md">Welcome to GOV.UK</h3>
//                                                 <div class="notranslate HGLrXd ojE3Fb">
//                                                   <!-- ... site link display info ... -->
//                                                     <cite class="tjvcx GvPZzd cHaqb" role="text">https://www.gov.uk</cite>
//                                                   <!-- ... more display info ... -->
//                                                 </div>
//                                                 <span jscontroller="IX53Tb" jsaction="rcuQ6b:npT2md" style="display:none"></span>
//                                             </a>
//                                             <!-- End of the first 'a' tag -->
//                                         </span>
//                                         <!-- ... potential other elements ... -->
//                                     </div>
//                                 </div>
//                                 <!-- End of the div matched by SearchResultLinkRegex -->
//                             </div>
//                             </div>
//                         </div>
//                         <!-- ... description snippet below ... -->
//                         <div class="IsZvec">
//                            <div class="VwiC3b yXK7lf p4wth r025kc hJNv6b Hdw6tb" style="-webkit-line-clamp:2">Services and information, benefits, includes eligibility, appeals, tax credits and Universal Credit, births, deaths, marriages and care.</div>
//                         </div>
//                     </div>
//                 </div>
//                  <!-- ... Sitelinks table follows ... -->
//                 <table class="jmjoTe wHYlTd" cellpadding="0" cellspacing="0"> ... </table>
//             </div>
//         </div>
//     </div>
// </div>