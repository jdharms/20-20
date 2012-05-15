using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace _20
{
    public class ConfigParser
    {
        private Dictionary<string, ConfigObject> configs;
        private XmlTextReader reader;
        private string filename;

        /// <summary>
        /// Parses out an XML configuation file.
        /// 
        /// </summary>
        /// <param name="filename">The file name of the XML Configuration file. Throws a FileNotFoundException if the file does not exist.</param>
        /// <param name="possibleTags">The configuation tags to look for.</param>
        public ConfigParser(string filename, string[] possibleTags)
        {
            try
            {
                this.filename = filename;
                configs = new Dictionary<string, ConfigObject>();
                foreach (string tag in possibleTags)
                {
                    if (resetReaderTo(tag))
                    {
                        List<string> indicators = this.getAllIndicators(tag);
                        Dictionary<string, string> hashtags = this.getAllHashtags(tag);
                        configs.Add(tag, new ConfigObject(indicators, hashtags));
                    }
                }
            }
            catch(FileNotFoundException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Searches for if a configuation was found for a specific tag. 
        /// </summary>
        /// <param name="tag">The tag to look for.</param>
        /// <returns>Returns true if a configuation is found. Otherwise false.</returns>
        public bool containsConfiguration(string tag)
        {
            ConfigObject config = null;
            if (configs.TryGetValue(tag, out config))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Searches for the indicators for a specific tag. If no indicators are found, this method returns null.
        /// </summary>
        /// <param name="tag">The tag to look for. Throws a ConfigurationNotFoundException if the tag is not found.</param>
        /// <returns>A list of indicators for a tag.</returns>
        public List<string> getIndicatorsForTag(string tag)
        {
            ConfigObject config = null;
            if (configs.TryGetValue(tag, out config))
            {
                return config.Indicators;
            }

            throw new ConfigurationNotFoundException(tag);
        }

        /// <summary>
        /// Searches for the hashtags for a specific tag. If no hashtags are found, this method returns null.
        /// </summary>
        /// <param name="tag">The tag to look for. Throws a ConfigurationNotFoundException if the tag is not found.</param>
        /// <returns>A dictionary mapping known hashtags to configured hashtags.</returns>
        public Dictionary<string, string> getHashtagsForTag(string tag)
        {
            ConfigObject config = null;
            if (configs.TryGetValue(tag, out config))
            {
                return config.Hashtags;
            }

            throw new ConfigurationNotFoundException(tag);
        }

        private List<string> getAllIndicators(string tag)
        {
            if (!this.hasIndicators(tag))
            {
                return null;
            }

            List<string> all = null; 
            if (reader.ReadToDescendant("text"))
            {
                all = new List<string>();
                all.Add(reader.ReadElementContentAsString());
            }
            while (reader.ReadToNextSibling("text"))
            {
                all.Add(reader.ReadElementContentAsString());
            }

            return all;
        }

        private bool hasIndicators(string tag)
        {
            this.resetReaderTo(tag);
            return reader.ReadToDescendant("indicator");
        }

        private Dictionary<string, string> getAllHashtags(string tag)
        {
            if (!this.hasHashtags(tag))
            {
                return null;
            }

            // ************** TODO ****************** //
            // Edit this if statement block so that it looks for these hashtags 
            string[] tagSearch;
            if (tag.Equals("made"))
            {
                tagSearch = new string[] { "fastbreak", "goaltending" };
            }
            else if (tag.Equals("missed"))
            {
                tagSearch = new string[] { "blocked" };
            }
            else
            {
                tagSearch = new string[0];
            }

            Dictionary<string, string> all = null;
            foreach (string hashtag in tagSearch)
            {
                if (reader.ReadToDescendant(hashtag))
                {
                    if (all == null)
                    { 
                        all = new Dictionary<string, string>();
                    }
                    all.Add(hashtag, reader.ReadElementContentAsString());
                }
                this.hasHashtags(tag);
            }

            return all;
        }

        private bool hasHashtags(string tag)
        {
            this.resetReaderTo(tag);
            return reader.ReadToDescendant("hashtag");
        }

        private bool resetReaderTo(string tag)
        {
            reader = new XmlTextReader(filename);
            reader.ReadToDescendant("setup");
            return reader.ReadToDescendant(tag);
        }

        private class ConfigObject
        {
            public List<string> Indicators { get; set; }
            public Dictionary<string, string> Hashtags { get; set; }

            public ConfigObject()
            {
                Indicators = new List<string>();
                Hashtags = new Dictionary<string, string>();
            }

            public ConfigObject(List<string> indicators, Dictionary<string, string> hashtags)
            {
                Indicators = indicators;
                Hashtags = hashtags;
            }
        }

        public class ConfigurationNotFoundException : Exception
        {
            private string tag;
            public ConfigurationNotFoundException(string tag)
                : base()
            {
                this.tag = tag;
            }

            public override string Message
            {
                get
                {
                    return "Configuration could not be found for \"" + tag + "\"";
                }
            }
        }

        public static void print(object o)
        {
            Console.WriteLine(o);
        }
    }
}
