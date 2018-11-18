using System.Collections.Generic;

namespace Cycles.Version2
{
    public class Vertex
{
    public int Id { get;set; }
    public int Index { get; set; }
    public int Lowlink { get; set; }

    public HashSet<Vertex> Dependencies { get; set; }

    public Vertex()
    {
        Id = -1;
        Index = -1;
        Lowlink = -1;
        Dependencies = new HashSet<Vertex>();
    }

    public override string ToString()
    {
        return string.Format("Vertex Id {0}", Id);
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        Vertex other = obj as Vertex;

        if (other == null)
            return false;

        return Id == other.Id;
    }
}
}