using DAL;
using Microsoft.EntityFrameworkCore;
using PracticeWebApiWithToken1.DataContext;

namespace PracticeWebApiWithToken1.Repository
{
    public class StudentRepostiry : IStudent
    {
        private readonly ApplicationDbContext _context;
        public StudentRepostiry(ApplicationDbContext context)
        {
                _context = context;
        }

        public Student AddStudent(Student student)
        {
          var result =  _context.students.Add(student);
            _context.SaveChanges();
            return result.Entity;
        }

        public Student DeleteStudent(int id)
        {
            var result = _context.students.Where(x => x.Id == id).FirstOrDefault();
             if (result != null)
             {
                 _context.students.Remove(result);
                 _context.SaveChanges();
                 return result;
             }
             else
             {
             return null;
             }
            
        }

        public Student GetStudentById(int id)
        {
            return _context.students.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Student> GetStudents()
        {
          return _context.students.ToList();
        }

        public Student UpdateStudent(Student student)
        {
            var result = _context.Update(student);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
