namespace TD
{
    public interface IPersistantData: IUniqueID, IPackableData {
        void ResetState();
    }
}