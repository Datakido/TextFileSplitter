ALERT!

You must have .NET 4.0 installed in order for this utility to work.

Command Line:

Usage: TextFileSplitter [options] -i=<filetosplit> -o=<destinationdir>
Options:

-h=<numberofheaders>      Tells the processor to insert the header into each file chunk.
                          It will be assumed one header line is wanted, if no number is assigned to 
                          this parameter.
-splitstrategy:           This tells the processor what strategy to use:
	ls:<lines> 	  		        Use the split by line strategy.
	kbs:<bytesize>   		    Use the split by size strategy.
	boundary:<checkthistext> 	Use the split by text boundary strategy.
	regex:<regextext>        	Used to tell the processor to check for a regex boundry.
	topchunk:<bytesize>         Used the split off one chunk strategy.
-filepattern=<pattern>    Used to tell the processor to name each file chunk using this pattern.
-boundaryasfilename       Used in conjunction with the boundary and regex strategies. Will use 
			              the boundary as the filename
-omitboundary             Used with regex and boundary strategies. Will omit the boundary text/line.
-testcontains             Used in conjunction with the boundary strategy. The match should be partial.
-testliteral              Used in conjunction with the boundary strategy. The match should be literal.

Example command-line for splitting a file using bytes:
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -h -splitstrategy:kbs:100000

Example command-line for splitting a file using line counts
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -h -splitstrategy:ls:1000

Example command-line for splitting a file using line counts and 3 header lines
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -h=3 -splitstrategy:ls:1000

Example command-line for splitting a file using a text boundary with partial match
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -splitstrategy:boundary:A001 -testcontains

Example command-line for splitting a file using a text boundary with literal match
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -splitstrategy:boundary:A001 -testliteral

Example command-line for splitting a file using a regular expression
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -splitstrategy:regex:^A001\|

Example command-line using file name patterns
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -filepattern:[FILENAME]_[SEQUENCE:0000]
Result: MonthlyUpdate20070625_0001

Example command-line for splitting a file using a regular expression and boundary as file name
-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -splitstrategy:regex:^\[ -boundaryasfilename

Each file will be appended with a dash, then the number in the sequence. Using the filename above it would look like this:

MonthlyUpdate20070625-1.txt
MonthlyUpdate20070625-2.txt
MonthlyUpdate20070625-3.txt

File naming conventions can use the following tokens to customize the filename for each chunk. THESE ARE CASE SENSITIVE!

* [FILENAME]    - The name of the file without the extension
* [SEQUENCE:0]  - The file chunck number. Each 0 is a placeholder for a digit.
* [DATE:format] - This will allow you to add a date in the format that you specify.
* [EXT]         - The file's extension.

The following characters can be used to separate parameters:
/ or - or --
-i, --i, or /i are all valid.

Parameter values can be delimited using = or :.
h=3 or h:3 are all valid.

Please refer all questions and comments to support@systemwidgets.com

For news and updates please visit http://www.systemwidgets.com
