using System;
using System.Linq;
using System.Collections.Generic;
	
namespace 作業客戶管理.Models
{   
	public  class 客戶統計Repository : EFRepository<客戶統計>, I客戶統計Repository
	{

	}

	public  interface I客戶統計Repository : IRepository<客戶統計>
	{

	}
}