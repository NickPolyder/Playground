using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Playground.WinForms.Database
{
	public partial class NorthwindEntities
	{
		public NorthwindEntities(bool log) : this()
		{
			Database.Log = (sql) => System.Diagnostics.Debug.WriteLine(sql);
		}
		public List<CatRole> GetCatRolesByRoleIds(string ids)
		{
			var array = ids.Split(',');
			return CatRoles.Where(c=>c.Id>3 && StringSplit(c.Roles,",").Any(r=> array.Contains(r))).ToList();
		}

	}
}