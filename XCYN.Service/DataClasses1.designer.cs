﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCYN.Service
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MeetingSys")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void Insertzcp_info(zcp_info instance);
    partial void Updatezcp_info(zcp_info instance);
    partial void Deletezcp_info(zcp_info instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::XCYN.Service.Properties.Settings.Default.MeetingSysConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<zcp_info> zcp_info
		{
			get
			{
				return this.GetTable<zcp_info>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.zcp_info")]
	public partial class zcp_info : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private System.Nullable<int> _category_id;
		
		private string _content;
		
		private string _formula_name;
		
		private System.Nullable<int> _right_id;
		
		private System.Nullable<int> _point;
		
		private System.Nullable<int> _b_id;
		
		private System.Nullable<int> _user_id;
		
		private System.Nullable<System.DateTime> _add_time;
		
		private System.Nullable<int> _click;
		
		private System.Nullable<int> _great_count;
		
		private System.Nullable<int> _state;
		
		private System.Nullable<int> _grade_id;
		
		private System.Nullable<int> _accuracy_id;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void Oncategory_idChanging(System.Nullable<int> value);
    partial void Oncategory_idChanged();
    partial void OncontentChanging(string value);
    partial void OncontentChanged();
    partial void Onformula_nameChanging(string value);
    partial void Onformula_nameChanged();
    partial void Onright_idChanging(System.Nullable<int> value);
    partial void Onright_idChanged();
    partial void OnpointChanging(System.Nullable<int> value);
    partial void OnpointChanged();
    partial void Onb_idChanging(System.Nullable<int> value);
    partial void Onb_idChanged();
    partial void Onuser_idChanging(System.Nullable<int> value);
    partial void Onuser_idChanged();
    partial void Onadd_timeChanging(System.Nullable<System.DateTime> value);
    partial void Onadd_timeChanged();
    partial void OnclickChanging(System.Nullable<int> value);
    partial void OnclickChanged();
    partial void Ongreat_countChanging(System.Nullable<int> value);
    partial void Ongreat_countChanged();
    partial void OnstateChanging(System.Nullable<int> value);
    partial void OnstateChanged();
    partial void Ongrade_idChanging(System.Nullable<int> value);
    partial void Ongrade_idChanged();
    partial void Onaccuracy_idChanging(System.Nullable<int> value);
    partial void Onaccuracy_idChanged();
    #endregion
		
		public zcp_info()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_category_id", DbType="Int")]
		public System.Nullable<int> category_id
		{
			get
			{
				return this._category_id;
			}
			set
			{
				if ((this._category_id != value))
				{
					this.Oncategory_idChanging(value);
					this.SendPropertyChanging();
					this._category_id = value;
					this.SendPropertyChanged("category_id");
					this.Oncategory_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_content", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string content
		{
			get
			{
				return this._content;
			}
			set
			{
				if ((this._content != value))
				{
					this.OncontentChanging(value);
					this.SendPropertyChanging();
					this._content = value;
					this.SendPropertyChanged("content");
					this.OncontentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_formula_name", DbType="VarChar(200)")]
		public string formula_name
		{
			get
			{
				return this._formula_name;
			}
			set
			{
				if ((this._formula_name != value))
				{
					this.Onformula_nameChanging(value);
					this.SendPropertyChanging();
					this._formula_name = value;
					this.SendPropertyChanged("formula_name");
					this.Onformula_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_right_id", DbType="Int")]
		public System.Nullable<int> right_id
		{
			get
			{
				return this._right_id;
			}
			set
			{
				if ((this._right_id != value))
				{
					this.Onright_idChanging(value);
					this.SendPropertyChanging();
					this._right_id = value;
					this.SendPropertyChanged("right_id");
					this.Onright_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_point", DbType="Int")]
		public System.Nullable<int> point
		{
			get
			{
				return this._point;
			}
			set
			{
				if ((this._point != value))
				{
					this.OnpointChanging(value);
					this.SendPropertyChanging();
					this._point = value;
					this.SendPropertyChanged("point");
					this.OnpointChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_b_id", DbType="Int")]
		public System.Nullable<int> b_id
		{
			get
			{
				return this._b_id;
			}
			set
			{
				if ((this._b_id != value))
				{
					this.Onb_idChanging(value);
					this.SendPropertyChanging();
					this._b_id = value;
					this.SendPropertyChanged("b_id");
					this.Onb_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_id", DbType="Int")]
		public System.Nullable<int> user_id
		{
			get
			{
				return this._user_id;
			}
			set
			{
				if ((this._user_id != value))
				{
					this.Onuser_idChanging(value);
					this.SendPropertyChanging();
					this._user_id = value;
					this.SendPropertyChanged("user_id");
					this.Onuser_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_add_time", DbType="DateTime")]
		public System.Nullable<System.DateTime> add_time
		{
			get
			{
				return this._add_time;
			}
			set
			{
				if ((this._add_time != value))
				{
					this.Onadd_timeChanging(value);
					this.SendPropertyChanging();
					this._add_time = value;
					this.SendPropertyChanged("add_time");
					this.Onadd_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_click", DbType="Int")]
		public System.Nullable<int> click
		{
			get
			{
				return this._click;
			}
			set
			{
				if ((this._click != value))
				{
					this.OnclickChanging(value);
					this.SendPropertyChanging();
					this._click = value;
					this.SendPropertyChanged("click");
					this.OnclickChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_great_count", DbType="Int")]
		public System.Nullable<int> great_count
		{
			get
			{
				return this._great_count;
			}
			set
			{
				if ((this._great_count != value))
				{
					this.Ongreat_countChanging(value);
					this.SendPropertyChanging();
					this._great_count = value;
					this.SendPropertyChanged("great_count");
					this.Ongreat_countChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_state", DbType="Int")]
		public System.Nullable<int> state
		{
			get
			{
				return this._state;
			}
			set
			{
				if ((this._state != value))
				{
					this.OnstateChanging(value);
					this.SendPropertyChanging();
					this._state = value;
					this.SendPropertyChanged("state");
					this.OnstateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_grade_id", DbType="Int")]
		public System.Nullable<int> grade_id
		{
			get
			{
				return this._grade_id;
			}
			set
			{
				if ((this._grade_id != value))
				{
					this.Ongrade_idChanging(value);
					this.SendPropertyChanging();
					this._grade_id = value;
					this.SendPropertyChanged("grade_id");
					this.Ongrade_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_accuracy_id", DbType="Int")]
		public System.Nullable<int> accuracy_id
		{
			get
			{
				return this._accuracy_id;
			}
			set
			{
				if ((this._accuracy_id != value))
				{
					this.Onaccuracy_idChanging(value);
					this.SendPropertyChanging();
					this._accuracy_id = value;
					this.SendPropertyChanged("accuracy_id");
					this.Onaccuracy_idChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
