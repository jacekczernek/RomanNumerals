# RomanNumerals
Roman Numerals conversion library

Assumptions that were made:
- When two numbers are next to each other ("345 543") thay need to be separate by ',' in the output text, so that they can be distinguish 
- Assuming that there are no fractions in text, in case of input like ("Lorem ipsum 2,45"), 2 and 45 will be treated as separate numbers
- When text contain number with is out of the range ("Lorem ipsum 4566"), out of range exception should be thrown

Build instructions:
- Open in VS and build
            
