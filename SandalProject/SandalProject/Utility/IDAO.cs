namespace SandalProject.Utility
{
    public interface IDAO
    {
        public List<Entity> ReadAll();

        public bool Delete(int id);

        public bool Insert(Entity e);

        public bool Update(Entity e);

        public Entity Find(int id);


    }
}
