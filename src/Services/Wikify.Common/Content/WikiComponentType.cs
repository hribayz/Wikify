using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.Content
{
    /// <summary>
    /// A type of the Wikify composition part as defined in <see cref="IWikiComponent"/>.
    /// </summary>
    public enum WikiComponentType
    {
        None, Article, ShortDescription, InfoPanel, Section, SubSection, Paragraph, Gallery, Image, LeadSection, BandLineupTimeline
    }
}
