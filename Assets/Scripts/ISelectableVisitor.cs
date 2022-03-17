public interface ISelectableVisitor
{
    void Visit(Platform platform);
    void Visit(Connector connector);
}
