# csharp-wim-technical-assessment
C# Technical Assessment for WIM Technologies

## Technical Assessment Disclaimer
This repository contains my personal solution to a private technical assessment. 
All code authored by me is intended solely for evaluation purposes by the hiring team. 
No license is granted for redistribution or reuse. 

How to run application
=
Clone repository  
.NET version 4.8.1 and above required  
Requires csv file as shown below  
Cell ID,Easting,Northing  
A,536660,183800  
File must be placed in bin/debug directory of application  

About Utility
=
This utility takes in a CSV file with ID, Easting and Northing values and assigns it frequencies.  
The list of frequencies are: 110, 111, 112, 113, 114, 115.  
It uses greedy coloring algorithm with an extra check to make sure no conflicts appear after assigning frequencies.  
The threshold decided is 200 meters given the dense cluster in testing data.  
