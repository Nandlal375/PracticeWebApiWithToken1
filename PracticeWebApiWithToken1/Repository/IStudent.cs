using DAL;

namespace PracticeWebApiWithToken1.Repository
{
    public interface IStudent
    {
        public List<Student> GetStudents();

        public Student GetStudentById(int id);

        public Student AddStudent(Student student);

        public Student UpdateStudent(Student student);
        public Student DeleteStudent(int id);
        //Task<IEnumerable<Student>> GetStudents();
    }
}
