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
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
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
            catch (Exception ex)
            {

                return false;
            }
            finally
            { 
              //add dispose here
            }
        }

        //client model comes from chtml page or controller page
        internal string addclient(client model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {

                //first check if the client name is already in use

                client client = db.clients.FirstOrDefault(u => u.Name == model.Name);


                // Check if user already exists
                if (client == null)
                {
                    //DO whatever work is required to check etc
                    db.clients.Add(client);
                    return "client added";
                }
                else
                {
                    return "A client with the same name already exists";
                }



            }
            catch (Exception ex)
            {

                return "failed";
            }
            finally
            {
                //add dispose here
            }
        }
    }
}