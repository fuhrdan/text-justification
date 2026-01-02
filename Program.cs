//*****************************************************************************
//** 68. Text Justification                                         leetcode **
//*****************************************************************************
//** Words pack tight                                                        **
//** Spaces    shared                                                        **
//** Greedy     lines                                                        **
//** Ends  left  true                                                        **
//*****************************************************************************

/**
 * Note: The returned array must be malloced, assume caller calls free().
 */
char** fullJustify(char** words, int wordsSize, int maxWidth, int* returnSize) {
    char** result = (char**)malloc(sizeof(char*) * wordsSize);
    int resIndex = 0;

    int i = 0;
    while(i < wordsSize)
    {
        int lineLen = 0;
        int start = i;

        /* Pack as many words as possible */
        while(i < wordsSize &&
              lineLen + strlen(words[i]) + (i - start) <= maxWidth)
        {
            lineLen += strlen(words[i]);
            i++;
        }

        int wordCount = i - start;
        char* line = (char*)malloc(maxWidth + 1);
        int pos = 0;

        /* Last line or single-word line → left justify */
        if(i == wordsSize || wordCount == 1)
        {
            for(int j = start; j < i; j++)
            {
                int len = strlen(words[j]);
                memcpy(line + pos, words[j], len);
                pos += len;

                if(j < i - 1)
                {
                    line[pos++] = ' ';
                }
            }

            while(pos < maxWidth)
            {
                line[pos++] = ' ';
            }
        }
        else
        {
            int spaces = maxWidth - lineLen;
            int gaps = wordCount - 1;
            int spacePerGap = spaces / gaps;
            int extraSpaces = spaces % gaps;

            for(int j = start; j < i; j++)
            {
                int len = strlen(words[j]);
                memcpy(line + pos, words[j], len);
                pos += len;

                if(j < i - 1)
                {
                    int spaceCount = spacePerGap;
                    if(j - start < extraSpaces)
                    {
                        spaceCount++;
                    }

                    for(int s = 0; s < spaceCount; s++)
                    {
                        line[pos++] = ' ';
                    }
                }
            }
        }

        line[maxWidth] = '\0';
        result[resIndex++] = line;
    }

    *returnSize = resIndex;
    return result;
}