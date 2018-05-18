using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author name: Andrew Hardie
//Description of class purpose: Moduels To add, retrieve and delete student objects
//Date last modified: 25/10/2017

namespace BusinessObjects
{
    public class ModuleList
    {
        private List<Student> _list = new List<Student>();

        public void add(Student newStudent)
        {
            _list.Add(newStudent);
        }

        public Student find(int matric)
        {
            foreach (Student s in _list)
            {
                if (matric == s.Matric)
                {
                    return s; 
                }
                
            }

            return null;
        }

        public void delete(int matric)
        {
            Student p = this.find(matric);
            if (p != null)
            {
                _list.Remove(p);
            }

        }

        public List<int> matrics
        {
            get
            {
               List<int> res = new List<int>();
               foreach(Student p in _list)
                   res.Add(p.Matric);
                return res;
            }        
        }
    }
}
