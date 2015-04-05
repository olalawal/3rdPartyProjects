using Domain.StatisticalSolutions.Domain.Models.Context;
using StatisticalSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatisticalSolutions.ViewModels;



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
                    throw new CustomException("CLIENT_ALLREADY_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
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
                    throw new CustomException("STUDENT_ALLREADY_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
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
                if (model != null)
                {
                     //first check if the client name is already in use
                    client client=new client();
                    DateTime currentDatetime = DateTime.Now;
                    student student = db.students.FirstOrDefault(u => u.Email == model.student.Email);

                    if (student == null)                   
                        model.student_id = addstudent(model.student); 
                    else                   
                        model.student_id = student.student_id; 
 
                    //set student to null to prevent duplicate insertion
                    if (student==null)
                    {
                        student = model.student;
                    }
                    model.student = null;

                    seminar seminar = db.seminars.FirstOrDefault(u => u.seminar_id == model.seminar_id);
                    if (seminar == null)
                    {
                        throw new CustomException("SEMINAR_NOT_FOUND");
                    }

                    if (!string.IsNullOrEmpty(model.client.Name))
                       client =  db.clients.FirstOrDefault(c => c.Name == model.client.Name);

                    if (client != null)
                        model.client_id = client.client_id;

                    //set client to null of model to prevent duplicate insertion                   
                    model.client = null;

              
                    //check user is registered for for seminar
                  if(seminar.registrations.Any(z => z.student_id == model.student_id && z.client_id == model.client_id))
                  {
                      throw new CustomException("STUDENT_ALLREADY_REGISTERED");
                  }
                
                //DO whatever work is required to check etc                   
                model.Registerdate = currentDatetime;
                db.registrations.Add(model);
                db.SaveChanges();
                // assign student and seminar to display at register complete page
                model.seminar = seminar;
                model.student = student;
                }
                return model.id;
            }
            catch (CustomException ex)
            {
                throw ex;
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
                if (model != null)
                {
                   if( string.IsNullOrEmpty(model.Description))
                       throw new CustomException("SEMINAR_DESCRIPTION_IS_NULL");

                      if( string.IsNullOrEmpty(model.TitleHtml))
                       throw new CustomException("SEMINAR_TITLEHTML_IS_NULL");

                    //TO do add more valiadtion i.e start date end date etc, location
                    seminar seminar = db.seminars.FirstOrDefault(s => s.TitleHtml == model.TitleHtml && s.StartDate == model.StartDate && s.Enddate == model.Enddate
                        && s.Address1 == model.Address1 && s.Address2 == model.Address2 && s.City == model.City && s.StateProvince == model.StateProvince && s.Country == model.Country);
                    
                    if (seminar==null)
                    { 
                        //DO whatever work is required to check etc
                        db.seminars.Add(model);
                        db.SaveChanges();
                        return model.seminar_id;                       
                    }
                    else
                    {
                        throw new CustomException("SEMINAR_ALLREADY_EXIST");
                    }
                }
                else
                {
                    throw new CustomException("SEMINAR_MODEL_SUPPLIED_IS_NULL");
                }
             
            }
            catch (CustomException ex)
            {
                throw ex;
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
        internal int addcontactmessage(message model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                // Check if user already exists
                if (model != null)
                {
                    if(string.IsNullOrEmpty(model.Body))
                        throw new CustomException("MESSAGE_BODY_IS_NULL");

                      if(string.IsNullOrEmpty(model.Email))
                          throw new CustomException("MESSAGE_EMAIL_IS_NULL");

                    DateTime currentDate =  DateTime.Now;
                    model.MessageDate = currentDate;
                    //DO whatever work is required to check etc
                    db.messages.Add(model);
                    db.SaveChanges();
                    return model.id;
                }
                else
                {
                    throw new CustomException("MESSAGE_MODEL_SUPPLIED_IS_NULL");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
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
        internal List<seminar> getseminars()
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<seminar> seminars = db.seminars.OrderBy(s=>s.TitleHtml).ToList();
                return seminars;
            }
            catch (CustomException ex)
            {
                throw ex;
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


        internal student getstudentbyid(int student_id)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                student student = db.students.FirstOrDefault(s => s.student_id == student_id);
                return student;
            }
            catch (CustomException ex)
            {
                throw ex;
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
            catch (CustomException ex)
            {
                throw ex;
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
                List<client> clients = db.clients.OrderBy(c=>c.Name).ToList();  
                return clients;
            }
            catch (CustomException ex)
            {
                throw ex;
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
                List<Countries> countries = db.Countries.OrderBy(c=>c.Country).ToList();
                return countries;
            }
            catch (CustomException ex)
            {
                throw ex;
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
                List<seminar> seminars = db.seminars.OrderBy(s=>s.StartDate).ToList();
                return seminars; 
            }
            catch (CustomException ex)
            {
                throw ex;
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