You: create a list of markdown file formatting options

Leo AI: Here is a comprehensive list of standard Markdown formatting options, ranging from basic text styling to advanced structural elements.

### **Text Emphasis & Styling**
These options modify how text appears inline.

*   **Bold**: Wrap text in double asterisks or double underscores.
    `**This is bold**` or `__This is bold__`
*   **Italic**: Wrap text in single asterisks or underscores.
    `*This is italic*` or `_This is italic_`
*   **Bold + Italic**: Use three asterisks or underscores.
    `***This is bold and italic***`
*   **Strikethrough**: Wrap text in double tildes.
    `~~This is deleted~~`
*   **Code (Inline)**: Wrap text in single backticks.
    `` `console.log("Hello")` ``
*   **Highlight**: Some flavors (like GitHub Flavored Markdown) support `==highlight==`.

### **Headings**
Construct headings using hash symbols (`#`) followed by a space. Use up to six levels.

*   **Level 1**: `# Heading 1`
*   **Level 2**: `## Heading 2`
*   **Level 3**: `### Heading 3`
*   ... down to ...
*   **Level 6**: `###### Heading 6`

### **Lists**
Markdown supports both unordered and ordered lists.

**Unordered Lists**
Use asterisks, plus signs, or hyphens.
```text
- Item one
- Item two
* Item three
+ Item four
```

**Ordered Lists**
Use numbers followed by a period.
```text
1. First step
2. Second step
3. Third step
```

**Task Lists**
Many renderers support checkboxes.
```text
- [ ] To-do item
- [x] Completed item
```

### **Blockquotes & Code**
Use these for quoting external text or writing code blocks.

**Blockquotes**
Use the greater-than symbol (`>`).
```text
> This is a quote.
> It can span multiple lines.
```

**Code Blocks**
Wrap code in triple backticks (` ``` `) with an optional language tag for syntax highlighting.
```bash
# Bash script
echo "Hello World"
```
```python
# Python example
def hello():
    print("Hello")
```

### **Links & Images**
Create connections and embed visual content.

**Links**
Syntax: `[Link Text](URL "Optional Title")`
```text
[Brave Search](https://brave.com)
```

**Images**
Start with an exclamation mark `!`, then follow the link syntax.
```text
![Alt text description](image-url.jpg "Optional Title")
```

### **Tables**
Create tables using pipes (`|`) and hyphens (`-`). The first row defines headers, the second defines alignment.

| Header 1 | Header 2 |
|---|---|
| Cell A | Cell B |
| Cell C | Cell D |

*Alignment controls:*
*   `:---` (Left align)
*   `---:` (Right align)
*   `:---:` (Center align)

### **Horizontal Rules**
Create a thematic break using three or more hyphens, asterisks, or underscores on a new line.
```text
---
```

### **Escaping Characters**
If you need to display a markdown symbol literally (like an asterisk or hash), place a backslash `\` before it.
```text
\* This will show an asterisk, not italics.
```

### **Advanced / Extended Features**
Depending on the Markdown flavor (e.g., GitHub Flavored Markdown, CommonMark), you may also find support for:
*   **Footnotes**: `[^1]` followed by `[^1]: Text at the bottom`.
*   **Details/Summary**: Collapsible sections using HTML tags.
*   **Math**: Inline math using `$` or block math using `$$` (often requires specific renderers like MathJax).

