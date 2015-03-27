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
                student student = db.students.FirstOrDefault(s => s.Email == model.Email);
                if(student==null)
                {
                    return false;
                }
                else
                {
                    registration registration = db.registrations.FirstOrDefault(u => u.student_id == student.student_id && u.seminar_id == seminarid);
                    // Check if user already exists
                    if (registration != null)
                    {

                        return true;
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
            finally
            {
                db.Dispose();
            }
        }

        //client model comes from chtml page or controller page
        internal int addclient(client model)
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
                    db.SaveChanges();
                    return client.client_id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        //client model comes from chtml page or controller page
        internal int addstudent(student model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {

                //first check if the client name is already in use

                student student = db.students.FirstOrDefault(u => u.Email == model.Email);


                // Check if user already exists
                if (student == null)
                {
                    //DO whatever work is required to check etc
                    db.students.Add(model);
                    db.SaveChanges();
                    return model.student_id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

               throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        //client model comes from chtml page or controller page
        internal int registerforseminarbystudentandseminarid(registration model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
               
                //first check if the client name is already in use
                client client=new client();
                DateTime currentDatetime = DateTime.Now;
                student student = db.students.FirstOrDefault(u => u.student_id == model.student_id);
                seminar seminar = db.seminars.FirstOrDefault(u => u.seminar_id == model.seminar_id);
                if (!string.IsNullOrEmpty(model.client.Name))
                   client =  db.clients.FirstOrDefault(c => c.Name == model.client.Name);

                if (client != null)
                {
                    model.client_id = client.client_id;
                    model.client = client;
                }
                   


                // oposite here student and seminar must exists
                if ((student != null & seminar != null) && !seminar.registrations.Any(z => z.student_id == student.student_id))
                {
                    //DO whatever work is required to check etc
                    model.seminar = seminar;
                    model.student = student;
                    model.Registerdate = currentDatetime;
                    db.registrations.Add(model);
                    return model.id;
                    //return "student registred for seminar";
                }
                else
                {
                    return 0;
                    //return "You are already registred for this seminar!";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        //client model comes from chtml page or controller page
        internal int addseminar(seminar model)
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
                    return model.seminar_id;
                    //return "Seminar created ";
                }
                else
                {
                    return 0;
                   // return "A seminar cannot be added withoute a discreption!!";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        //client model comes from chtml page or controller page
        internal string addcontactmessage(message model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                // Check if user already exists
                if (model != null && model.Body !="" && model.Email !=null )
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
                db.Dispose();
            }
        }


        //client model comes from chtml page or controller page
        internal List<seminar> getseminars()
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<seminar> seminars = db.seminars.ToList();
                return seminars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        internal seminar getseminarbyid(int seminar_id)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                seminar seminar = db.seminars.FirstOrDefault(s => s.seminar_id == seminar_id);
                return seminar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        //code to gets all componies list
        internal List<client> getCompanies()  
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<client> clients = db.clients.ToList();  
                return clients;
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }


        internal List<Countries> getCountries() 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<Countries> countries = db.Countries.ToList();
                return countries;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }


        internal List<seminar> getfuturesemnarsstartdate()  
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
               //getting all seminars now but will change code later for only future seminars
                List<seminar> seminars = db.seminars.ToList();
                return seminars; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }
        


        

    }
}