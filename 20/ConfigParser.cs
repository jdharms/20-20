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
        private Dictionary<string, Dictionary<string, List<string>>> configs;
        private XmlTextReader reader;
        private string filename;

        /// <summary>
        /// Parses out an XML configuation file.
        /// 
        /// </summary>
        /// <param name="filename">The file name of the XML Configuration file. Throws a FileNotFoundException if the file does not exist.</param>
        /// <param name="possibleTags">The configuation tags to look for.</param>
        public ConfigParser(string filename)
        {
            Dictionary<string, string[]> tagsToTags = new Dictionary<string, string[]>();
            configs = new Dictionary<string, Dictionary<string, List<string>>>();

            tagsToTags.Add("events", new string[] { 
                "made", "missed", "blocked", 
                "rebound", "jumpball", "foul", 
                "turnover", "substitution", "timeout" 
            });

            tagsToTags.Add("hashtags", new string[] {
                "assist", "fastbreak", "goaltend", "ejected", "forced"
            });

            tagsToTags.Add("shotTypes", new string[] {
                "jumpshot", "layup", "dunk", "freethrow", "tip"
            });

            tagsToTags.Add("timeoutTypes", new string[] {
                "media", "official", "home", "away"
            });

            tagsToTags.Add("teamTypes", new string[] {
                "home", "away"
            });

            tagsToTags.Add("foulTypes", new string[] {
                "blocking", "charging", "shooting", 
                "offensive", "personal", "technical", "flagrant"
            });

            tagsToTags.Add("turnoverTypes", new string[] {
                "traveling", "lost", "bounds", "violation", "goaltending", "thrown"
            });

            try
            {
                this.filename = filename;
                foreach (string tag in tagsToTags.Keys)
                {
                    Dictionary<string, List<string>> thisConfig = null; 
                    foreach (string subTag in tagsToTags[tag])
                    {
                        if (resetReaderTo(subTag))
                        {
                            List<string> indicators = this.getAllIndicators(tag, subTag);
                            if (indicators != null)
                            {
                                if (thisConfig == null)
                                {
                                    thisConfig = new Dictionary<string, List<string>>();
                                }
                                thisConfig.Add(subTag, indicators);
                            }
                        }
                    }
                    if (thisConfig != null)
                    {
                        configs.Add(tag, thisConfig);
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
            Dictionary<string, List<string>> config = new Dictionary<string, List<string>>(); 
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
        public Dictionary<string, List<string>> getDictionaryForTag(string tag)
        {
            Dictionary<string, List<string>> config = new Dictionary<string, List<string>>(); 
            if (configs.TryGetValue(tag, out config))
            {
                return config;
            }

            throw new ConfigurationNotFoundException(tag);
        }

        private List<string> getAllIndicators(string tag, string subTag)
        {
            if (!this.hasIndicators(tag, subTag))
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

        private bool hasIndicators(string tag, string subTag)
        {
            this.resetReaderTo(tag, subTag);
            return reader.ReadToDescendant("indicator");
        }

        private bool resetReaderTo(string tag)
        {
            reader = new XmlTextReader(filename);
            reader.ReadToDescendant("setup");
            return reader.ReadToDescendant(tag);
        }

        private bool resetReaderTo(string tag, string subTag)
        {
            if (resetReaderTo(tag))
            {
                return reader.ReadToDescendant(subTag);
            }
            return false;
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
