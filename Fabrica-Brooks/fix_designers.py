import os
import re

PROJECT_DIR = "SILC.Web"

# Standard mappings
ASP_MAPPING = {
    "Label": "System.Web.UI.WebControls.Label",
    "TextBox": "System.Web.UI.WebControls.TextBox",
    "Button": "System.Web.UI.WebControls.Button",
    "LinkButton": "System.Web.UI.WebControls.LinkButton",
    "ImageButton": "System.Web.UI.WebControls.ImageButton",
    "HyperLink": "System.Web.UI.WebControls.HyperLink",
    "DropDownList": "System.Web.UI.WebControls.DropDownList",
    "ListBox": "System.Web.UI.WebControls.ListBox",
    "CheckBox": "System.Web.UI.WebControls.CheckBox",
    "RadioButton": "System.Web.UI.WebControls.RadioButton",
    "Literal": "System.Web.UI.WebControls.Literal",
    "Panel": "System.Web.UI.WebControls.Panel",
    "UpdatePanel": "System.Web.UI.UpdatePanel",
    "ScriptManager": "System.Web.UI.ScriptManager",
    "Timer": "System.Web.UI.Timer",
    "HiddenField": "System.Web.UI.WebControls.HiddenField",
    "FileUpload": "System.Web.UI.WebControls.FileUpload",
    "GridView": "System.Web.UI.WebControls.GridView",
    "Repeater": "System.Web.UI.WebControls.Repeater",
    "DataList": "System.Web.UI.WebControls.DataList",
    "Image": "System.Web.UI.WebControls.Image",
    "Calendar": "System.Web.UI.WebControls.Calendar",
    "ValidationSummary": "System.Web.UI.WebControls.ValidationSummary",
    "RequiredFieldValidator": "System.Web.UI.WebControls.RequiredFieldValidator",
    "CompareValidator": "System.Web.UI.WebControls.CompareValidator",
    "RegularExpressionValidator": "System.Web.UI.WebControls.RegularExpressionValidator",
    "CustomValidator": "System.Web.UI.WebControls.CustomValidator",
    "Chart": "System.Web.UI.DataVisualization.Charting.Chart",
    "ToolkitScriptManager": "AjaxControlToolkit.ToolkitScriptManager",
    "ModalPopupExtender": "AjaxControlToolkit.ModalPopupExtender",
    "CalendarExtender": "AjaxControlToolkit.CalendarExtender",
    "FilteredTextBoxExtender": "AjaxControlToolkit.FilteredTextBoxExtender",
    "AutoCompleteExtender": "AjaxControlToolkit.AutoCompleteExtender",
    "TabContainer": "AjaxControlToolkit.TabContainer",
    "TabPanel": "AjaxControlToolkit.TabPanel",
}

# Known user controls (TagName -> ClassName)
# Assuming they are registered with consistent names or we infer from file
USER_CONTROLS = {
    "INTEIRO": "SILC.Web.forms.INTEIRO",
    "INTEIRO2": "SILC.Web.forms.INTEIRO2",
    "INTEIRO7": "SILC.Web.forms.INTEIRO7",
    "DATA": "SILC.Web.forms.DATA",
    "MOEDA": "SILC.Web.forms.MOEDA",
    "CAMINHAO": "SILC.Web.forms.CAMINHAO",
    "CLIENTE": "SILC.Web.forms.CLIENTE",
    "CLIENTESCONTROL": "SILC.Web.forms.CLIENTESCONTROL",
    "DESTINOFINAL": "SILC.Web.forms.DESTINOFINAL",
    "GRUPORESIDUO": "SILC.Web.forms.GRUPORESIDUO",
    "DIA": "SILC.Web.forms.DIA",
    "INTEIROMES": "SILC.Web.forms.INTEIROMES",
    "MOTORISTA": "SILC.Web.forms.MOTORISTA",
}

def scan_file(filepath):
    with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
        content = f.read()

    controls = []
    
    # Regex to find Register directives
    # <%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
    registers = {}
    reg_matches = re.findall(r'<%@ Register\s+(?:src="([^"]+)"\s+)?tagname="([^"]+)"\s+tagprefix="([^"]+)"', content, re.IGNORECASE)
    for src, tagname, tagprefix in reg_matches:
        registers[f"{tagprefix}:{tagname}"] = tagname
    
    # Regex to find controls with ID and runat="server"
    # Handles <prefix:TagName ... ID="X" ... runat="server">
    # Simplified regex, might miss edge cases
    
    # Pattern 1: <Tag ... ID="X" ... runat="server" ... >
    pattern = r'<(\w+:\w+)\s+[^>]*ID="(\w+)"[^>]*runat="server"'
    matches = re.findall(pattern, content, re.IGNORECASE)
    
    for tag, ctrl_id in matches:
        controls.append((tag, ctrl_id))
        
    # Pattern 2: <Tag ... runat="server" ... ID="X" ... >
    pattern2 = r'<(\w+:\w+)\s+[^>]*runat="server"[^>]*ID="(\w+)"'
    matches2 = re.findall(pattern2, content, re.IGNORECASE)
    
    for tag, ctrl_id in matches2:
        controls.append((tag, ctrl_id))

    return controls, registers

def update_designer(designer_path, missing_controls):
    if not missing_controls:
        return
        
    with open(designer_path, 'r', encoding='utf-8') as f:
        content = f.read()
        
    # Find the last field declaration or the class end
    # We'll just insert before the last closing brace of the class
    
    lines = content.splitlines()
    insert_idx = -1
    
    # Find the class closing brace. It's usually indented.
    # Assuming standard formatting: 
    #     }
    # }
    
    # A safer bet is to look for the last field and insert after it.
    last_field_idx = -1
    for i, line in enumerate(lines):
        if "protected global::" in line:
            last_field_idx = i
            
    if last_field_idx != -1:
        insert_idx = last_field_idx + 1
    else:
        # Fallback: find class declaration and insert at end?
        # Too risky without parsing.
        # Let's search for the line with just "    }" or "  }" inside the namespace
        for i in range(len(lines) - 1, -1, -1):
            if re.match(r'^\s+}\s*$', lines[i]):
                # This might be namespace close or class close.
                # Usually class close is second to last }
                insert_idx = i
                break
                
    if insert_idx == -1:
        print(f"Could not find insertion point in {designer_path}")
        return

    new_lines = []
    for tag, ctrl_id in missing_controls:
        # Determine type
        prefix, name = tag.split(':')
        
        type_name = "System.Web.UI.Control" # Fallback
        
        if prefix.lower() == "asp":
            type_name = ASP_MAPPING.get(name, f"System.Web.UI.WebControls.{name}")
        elif name in USER_CONTROLS:
            type_name = USER_CONTROLS[name]
        else:
             # Try to guess or just use Control
             pass
             
        decl = f'''
        /// <summary>
        /// {ctrl_id} control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::{type_name} {ctrl_id};
'''
        new_lines.append(decl)
        
    # Insert
    lines[insert_idx:insert_idx] = new_lines
    
    with open(designer_path, 'w', encoding='utf-8') as f:
        f.write('\n'.join(lines))
    print(f"Updated {designer_path} with {len(new_lines)} fields.")

def main():
    print(f"Scanning {PROJECT_DIR} from {os.getcwd()}")
    count = 0
    for root, dirs, files in os.walk(PROJECT_DIR):
        for file in files:
            if file.lower().endswith('.aspx') or file.lower().endswith('.ascx'):
                count += 1
                filepath = os.path.join(root, file)
                designer_path = filepath + ".designer.cs" # e.g. .aspx.designer.cs
                
                # Some files might be .aspx.cs and .aspx.designer.cs, but filepath is .aspx
                if not os.path.exists(designer_path):
                    # Try replacing extension? No, usually it's appended.
                    # Check if .designer.cs exists by replacing extension
                    alt_path = os.path.splitext(filepath)[0] + ".designer.cs"
                    if os.path.exists(alt_path):
                        designer_path = alt_path
                    else:
                        continue
                
                controls, registers = scan_file(filepath)
                
                # Check what is missing
                with open(designer_path, 'r', encoding='utf-8') as f:
                    designer_content = f.read()
                    
                missing = []
                for tag, ctrl_id in controls:
                    # Check if ID is in designer
                    # Simple check
                    if f" {ctrl_id};" not in designer_content and f" {ctrl_id} " not in designer_content:
                        missing.append((tag, ctrl_id))
                        
                if missing:
                    print(f"File: {file}, Missing: {len(missing)}")
                    update_designer(designer_path, missing)

if __name__ == "__main__":
    main()
