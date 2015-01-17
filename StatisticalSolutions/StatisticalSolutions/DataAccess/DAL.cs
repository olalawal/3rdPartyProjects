using Domain.StatisticalSolutions.Domain.Models.Context;
using StatisticalSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatisticalSolutions.DataAccess
{
    public class DAL
    {
        //sample method

        internal bool CheckIfStudentIsRegistereredForSeminar(student model, int seminarid)
        {
            try
            {

                using (StatisticalSolutionsContext db = new StatisticalSolutionsContext())
                {
                    student user = db.students.FirstOrDefault(u => u.id == model.id);
                    // Check if user already exists
                    if (user == null)
                    {
                       //DO whatever work is required to check etc

                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                
                return false;
            }
        }


    }
}