using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Visitors.TypeScript;

namespace CrowdFundingAPI.Typings;

public class Visitor : TypeScriptExportVisitor
{
    public Visitor(TextWriter writer, ExportContext exportContext)
        : base(writer, exportContext)
    {
    }

    public override void Visit(RtField node)
    {
        base.Visit(node);
    }
}