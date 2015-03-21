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

                client client = db.clients.Where(u => u.Name == model.Name).FirstOrDefault();


                // Check if user already exists
                if (client == null)
                {
                    //DO whatever work is required to check etc
                    db.clients.Add(client);
                    db.SaveChanges();
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

        //client model comes from chtml page or controller page
        internal string addstudent(student model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {

                //first check if the client name is already in use

                student student = db.students.Where(u => u.Email == model.Email).FirstOrDefault();


                // Check if user already exists
                if (student == null)
                {
                    //DO whatever work is required to check etc
                    db.students.Add(student);
                    db.SaveChanges();
                    return "student added";
                }
                else
                {
                    return "A student with the same email already exists";
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

        //client model comes from chtml page or controller page
        internal string registerforseminarbystudentandseminarid(registration model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {

                

                //first check if the client name is already in use

                student student = db.students.Where(u => u.id == model.student_id).FirstOrDefault();
                seminar seminar = db.seminars.Where(u => u.id == model.seminar_id).FirstOrDefault();


                // oposite here student and seminar must exists
                if ((student != null & seminar !=null) &&  seminar.registrations.Any(z=>z.student_id != student.id))
                {
                    

                    //DO whatever work is required to check etc
                    db.registrations.Add(model);
                    
                    return "student registred for seminar";
                }
                else
                {
                    return "You are already registred for this seminar!";
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

        //client model comes from chtml page or controller page
        internal string addseminar(seminar model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {



         


                // Check if user already exists and is not alrady registered
                if (model != null && model.Description!="")
                {
                    //TO do add more valiadtion i.e start date end date etc, location

                    //DO whatever work is required to check etc
                    db.seminars.Add(model);
                    db.SaveChanges();
                    return "Seminar created ";
                }
                else
                {
                    return "A seminar cannot be added withoute a discreption!!";
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

        //client model comes from chtml page or controller page
        internal string addcontactmessage(message model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {

               

                // Check if user already exists
                if (model == null && model.Body !="" && model.Email !=null )
                {
                    //DO whatever work is required to check etc
                    db.messages.Add(model);
                    db.SaveChanges();
                    return "message Sent";
                }
                else
                {
                    return "Message must contain email body and and address";
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