using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace TextFileSplitterConsole
{
    /// <summary>
    /// Arguments class
    /// </summary>
    public class Arguments
    {
        // Variables
        private readonly StringDictionary Parameters;
        private readonly Hashtable IntParameters = new Hashtable();
        private readonly int _index;

        // Constructor
        public Arguments(IEnumerable<string> Args)
        {
            Parameters = new StringDictionary();
            var Spliter = new Regex(@"^-{1,2}|^/|=|:",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var Remover = new Regex(@"^['""]?(.*?)['""]?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string Parameter = null;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))
            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" 
            //   /param4=happy -param5 '--=nice=--'
            foreach (string Txt in Args)
            {
                // Look for new parameters (-,/ or --) and a
                // possible enclosed value (=,:)
                string[] Parts = Spliter.Split(Txt, 3);

                switch (Parts.Length)
                {
                    // Found a value (for the last parameter 
                    // found (space separator))
                    case 1:
                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                            {
                                Parts[0] = Remover.Replace(Parts[0], "$1");

                                Parameters.Add(Parameter, Parts[0]);
                                IntParameters.Add(_index, Parts[0]);

                                _index += 1;
                            }

                            Parameter = null;
                        }

                        // else Error: no parameter waiting for a value (skipped)
                        break;

                    // Found just a parameter
                    case 2:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                            {
                                Parameters.Add(Parameter, "true");
                                IntParameters.Add(_index, Parameter);

                                _index += 1;
                            }
                        }

                        Parameter = Parts[1];
                        break;

                    // Parameter with enclosed value
                    case 3:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                            {
                                Parameters.Add(Parameter, "true");
                                IntParameters.Add(_index, Parameter);

                                _index += 1;
                            }
                        }

                        Parameter = Parts[1];

                        // Remove possible enclosing characters (",')
                        if (!Parameters.ContainsKey(Parameter))
                        {
                            Parts[2] = Remover.Replace(Parts[2], "$1");
                            Parameters.Add(Parameter, Parts[2]);
                            IntParameters.Add(_index, Parts[2]);

                            _index += 1;
                        }

                        Parameter = null;
                        break;
                }
            }

            // In case a parameter is still waiting
            if (Parameter != null)
            {
                if (!Parameters.ContainsKey(Parameter))
                {
                    Parameters.Add(Parameter, "true");
                    IntParameters.Add(_index, Parameter);

                    _index += 1;
                }
            }
        }

        // Retrieve a parameter value if it exists 
        // (overriding C# indexer property)
        public string this[string Param]
        {
            get
            {
                return (Parameters[Param]);
            }
        }

        public string this[int Index]
        {
            get
            {
                return IntParameters[Index].ToString();
            }
        }

        public int Count
        {
            get { return Parameters.Count; }
        }
    }
}
