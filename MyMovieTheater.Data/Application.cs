namespace MyMovieTheater.Data
{
    public class Application
    {
        public static MyMovieTheaterEntity GetDatabaseInstance()
        {
            return new MyMovieTheaterEntity();
        }
    }
}