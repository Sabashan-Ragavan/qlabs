# qlabs
Question 2: 

My implementation for question 2 involves calculating a score for each of the strings in the string array and then storing each of 
these strings, along with their score into a BST which is sorted based on their score. 
The score for each string is determined similar to calculating a value of a base N character array. The only difference is that 
the priority of each character is set by us, based on their lexicographical order, where a character's priority is equal to it's index in 
the string plus 1. My solution has been tested on various test cases and does not seem to fail.

Assuming n is the number of strings in the array and m is the maximum length of a string. 
If m > logn, then the run-time will be O(n*m).
if logn > m then the run-time will O(n*logn).

I came up with a few other solutions to solve the problem, but deemed the solution described as the one with the best run time. 
I believe a modified trie can also be used to solve the problem. A trie would promise O(m*n) run-time. 

Navigate to: QualiaLabs_Question2/QualiaLabs_Question2/Program.cs to find solution. 




