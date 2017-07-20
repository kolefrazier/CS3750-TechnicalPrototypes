//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CS3750TechnicalPrototypes.Models;
//using Microsoft.AspNetCore.Session;
//using Microsoft.AspNetCore.Http;

//namespace CS3750TechnicalPrototypes.Views.Services
//{
//	public class UserService
//	{
//		public void SetLoginInformation(Bidder user)
//		{
//			//Totally not susceptible to cookie modification.........
//			HttpContext.Session.SetString("LoggedIn", "true");
//			HttpContext.Session.SetInt32("UserId", user.BidderID);
//			HttpContext.Session.SetInt32("RoleId", RoleEnumToInt(user.Role.UserRole));
//			HttpContext.Session.SetString("RoleName", RoleEnumToString(user.Role.UserRole));
//			HttpContext.Session.SetString("UserEmail", user.EmailAddress);
//		}
//	}
//}
