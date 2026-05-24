// See https://aka.ms/new-console-template for more information

using System.Text;

var location = args[0];

Console.WriteLine(location);

var files = Directory.GetFiles(location, "*.cs", SearchOption.AllDirectories); 
var sb = new StringBuilder();
var sbSummary = new StringBuilder();

files.Sort((a, b) => String.Compare(Path.GetFileName(a), Path.GetFileName(b), StringComparison.InvariantCulture));
sb.AppendLine("# R3D.NET 0.9.1 unofficial Cheatsheet");
sb.AppendLine("## What is R3D?");
sb.AppendLine("R3D is an extension library for <a href=\"https://www.raylib.com/\">raylib</a> that expands its 3D capabilities, including rendering, lighting, kinematics, mesh utilities, and related helpers, without turning raylib into a full engine.");
sb.AppendLine("### Key Features\n\n- **Hybrid Renderer**: Deferred pipeline with forward rendering for transparency.\n- **Advanced Materials**: Complete PBR material system (Burley/SchlickGGX)\n- **Custom Shaders**: Support for surface shaders (materials/decals) and screen shaders.\n- **Dynamic Lighting**: Directional, spot, and omni lights with soft shadows\n- **Image-Based Lighting**: Supports environment IBL and reflection probes.\n- **Post-Processing**: SSAO, SSR, DoF, bloom, fog, tonemapping, and more\n- **Kinematics Support**: Basic kinematic system with capsule and mesh-based colliders.\n- **Mesh Utilities**: Mesh generation, manipulation, and helper utilities.\n- **Model Loading**: Assimp integration with animations and mesh generation\n- **Performance**: Built-in frustum culling, instanced rendering, and more");
sb.AppendLine();
sb.AppendLine("Information source: [Bigfoot71/r3d GitHub](https://github.com/Bigfoot71/r3d)");
sb.AppendLine("## What is this and why should I care?");
sb.AppendLine("This is a cheatsheet for the R3D.NET library. Keep in mind this is not manually written. Also it's not the official one. I just did this because the lack of a nice overview. So, I made a script that extracted this information. Anyway! Enjoy!");
sb.AppendLine();
sb.AppendLine("For more information or how to install, please visit the [GitHub repository](https://github.com/graphnode/r3d-cs).");
sb.AppendLine();

sb.AppendLine("## Examples");

sb.AppendLine("Below is a list of all the examples in the library. Those will link directly towards the source code on GitHub.");

foreach (var file in files.Where(f => f.Contains("Examples") && !f.Contains("Program.cs")))
{
    sb.AppendLine($"- [{Path.GetFileName(file)}](https://github.com/graphnode/r3d-cs/blob/master/Examples/{Path.GetFileName(file)})");

}

sb.AppendLine("## Functions");
sb.AppendLine("Below is a list of all the functions in the library.");
foreach (var file in files.Where(f => f.Contains("interop")))
{
    Console.WriteLine("Reading file: " + file);
    var name = Path.GetFileName(file).Replace(".g.cs", "");
    sb.AppendLine($"### {name} Functions");
    sb.AppendLine("```csharp");

    var content = File.ReadAllLines(file);

    bool summery = false;
    bool getFirstLine = false;
    var comment = "";
    
    foreach (var line in content)
    {
        if (line.Contains("<summary>"))
        {
            summery = true;
            sbSummary.Clear();
            continue;
        }

        if (summery && line.Contains("</summary>"))
        {
            summery = false;
            comment = sbSummary.ToString();
            getFirstLine = true;
            continue;
        }
        
        if (summery)
        {
            var toAdd = line
                .Replace("///", "")
                .Replace("<para>", "")
                .Replace("</para>", "")
                .Replace("<list type=\"bullet\">", "")
                .Replace("</list>", "")
                .Replace("<item><description>" , " - ")
                .Replace("</description></item>", "").Trim();
            if (toAdd.Length > 0)
            sbSummary.AppendLine(toAdd);
        }

        if (getFirstLine && line.Contains("public static"))
        {
            var code = line.Trim().Replace("public static partial ", "");
            var pos = code.IndexOf(" ", StringComparison.InvariantCulture);
            code = code.Insert(pos + 1, "R3D.");
            sb.AppendLine("/* " + comment.Substring(0, comment.Length - 1) + " */\n" + code);
            getFirstLine = false;
            sb.AppendLine();
            continue;
        }
        
        
    }
    sb.AppendLine("```");
}


sb.AppendLine("## Enums");
sb.AppendLine("Below is a list of all the enums in the library.");


foreach (var file in files.Where(f => f.Contains("enums")))
{
    Console.WriteLine("Reading file: " + file);
    var name = Path.GetFileName(file).Replace(".g.cs", "");
    sb.AppendLine($"### {name} enum");

    var content = File.ReadAllLines(file);
    sb.AppendLine("```csharp");
    var ignore = true;
    foreach (var line in content)
    {
        if (line.Contains("<summary>"))
        {
            ignore = false;
            continue;
        }
        
        if (line.Contains("</summary>") || line.Contains("<para>") || line.Contains("</para>") || line.Contains("<list type=\"bullet\">") || line.Contains("</list>"))
            continue;

        if (line.Contains("<remarks>"))
            ignore = true;

        if (line.Contains("</remarks>"))
        {
            ignore = false;
            continue;
        }

        if (!ignore && line.Length > 0)
        {
            sb.AppendLine(line.Replace("<item><description>", " - ").Replace("</description></item>", ""));
        }


    }

    sb.AppendLine("```");

}



sb.AppendLine("## Struct / delegate types");
sb.AppendLine("Below is a list of all the types in the library.");

foreach (var file in files.Where(f => f.Contains("types")))
{
    Console.WriteLine("Reading file: " + file);
    var name = Path.GetFileName(file).Replace(".g.cs", "");
    sb.AppendLine($"### {name} " + (name.Contains("Callback") ? "  delegate" : "  struct"));

    var content = File.ReadAllLines(file);
    sb.AppendLine("```csharp");
    var ignore = true;
    foreach (var line in content)
    {
        if (line.Contains("<summary>"))
        {
            ignore = false;
            continue;
        }
        
        if (line.Contains("</summary>") || line.Contains("<para>") || line.Contains("</para>") || line.Contains("<list type=\"bullet\">") || line.Contains("</list>") || line.Contains("[StructLayout(LayoutKind.Sequential)]") || line.Contains("[UnmanagedFunctionPointer(CallingConvention.Cdecl)]"))
            continue;

        if (line.Contains("<remarks>"))
            ignore = true;

        if (line.Contains("</remarks>"))
        {
            ignore = false;
            continue;
        }

        if (!ignore && line.Trim().Length > 0)
        {
            sb.AppendLine(line.Replace("<item><description>", " - ").Replace("</description></item>", ""));
        }


    }

    sb.AppendLine("```");

}

File.WriteAllText("output.md", sb.ToString().Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&"));