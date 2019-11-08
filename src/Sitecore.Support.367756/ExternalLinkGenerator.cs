namespace Sitecore.Support.XA.Feature.SiteMetadata.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Data.Items;
    using Sitecore.DependencyInjection;
    using Sitecore.Links;
    using Sitecore.Web;
    using Sitecore.XA.Feature.SiteMetadata.Services;
    using Sitecore.XA.Foundation.Multisite;
    using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
    using System;
    using System.Globalization;

    public class ExternalLinkGenerator : LinkProvider.LinkBuilder, IExternalLinkGenerator
    {
        public ExternalLinkGenerator()
            : base(new UrlOptions
            {
                AlwaysIncludeServerUrl = true
            })
        {
        }

        public string GetExternalUrl(Item item)
        {
            SiteInfo siteInfo = ServiceLocator.ServiceProvider.GetService<ISiteInfoResolver>().GetSiteInfo(item);
            string itemUrl = item.GetItemUrl();
            if (!string.IsNullOrWhiteSpace(siteInfo.TargetHostName))
            {
                Uri uri = new Uri(itemUrl);
                return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", uri.Scheme, siteInfo.TargetHostName, uri.PathAndQuery);
            }
            return itemUrl;
        }
    }
}