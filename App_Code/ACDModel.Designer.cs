﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Acig_Help_DeskModel", "FK_Sub_Categories_Categories", "Category", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Acig_Help_DeskModel.Category), "Sub_Categories", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Acig_Help_DeskModel.Sub_Categories), true)]

#endregion

namespace Acig_Help_DeskModel
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class Acig_Help_DeskEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new Acig_Help_DeskEntities object using the connection string found in the 'Acig_Help_DeskEntities' section of the application configuration file.
        /// </summary>
        public Acig_Help_DeskEntities() : base("name=Acig_Help_DeskEntities", "Acig_Help_DeskEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Acig_Help_DeskEntities object.
        /// </summary>
        public Acig_Help_DeskEntities(string connectionString) : base(connectionString, "Acig_Help_DeskEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Acig_Help_DeskEntities object.
        /// </summary>
        public Acig_Help_DeskEntities(EntityConnection connection) : base(connection, "Acig_Help_DeskEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Category> Categories
        {
            get
            {
                if ((_Categories == null))
                {
                    _Categories = base.CreateObjectSet<Category>("Categories");
                }
                return _Categories;
            }
        }
        private ObjectSet<Category> _Categories;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Sub_Categories> Sub_Categories
        {
            get
            {
                if ((_Sub_Categories == null))
                {
                    _Sub_Categories = base.CreateObjectSet<Sub_Categories>("Sub_Categories");
                }
                return _Sub_Categories;
            }
        }
        private ObjectSet<Sub_Categories> _Sub_Categories;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Categories EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCategories(Category category)
        {
            base.AddObject("Categories", category);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Sub_Categories EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToSub_Categories(Sub_Categories sub_Categories)
        {
            base.AddObject("Sub_Categories", sub_Categories);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Acig_Help_DeskModel", Name="Category")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Category : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="created_At">Initial value of the Created_At property.</param>
        /// <param name="updated_At">Initial value of the Updated_At property.</param>
        public static Category CreateCategory(global::System.Int64 id, global::System.String name, global::System.DateTime created_At, global::System.DateTime updated_At)
        {
            Category category = new Category();
            category.Id = id;
            category.Name = name;
            category.Created_At = created_At;
            category.Updated_At = updated_At;
            return category;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int64 _Id;
        partial void OnIdChanging(global::System.Int64 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Created_At
        {
            get
            {
                return _Created_At;
            }
            set
            {
                OnCreated_AtChanging(value);
                ReportPropertyChanging("Created_At");
                _Created_At = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Created_At");
                OnCreated_AtChanged();
            }
        }
        private global::System.DateTime _Created_At;
        partial void OnCreated_AtChanging(global::System.DateTime value);
        partial void OnCreated_AtChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Updated_At
        {
            get
            {
                return _Updated_At;
            }
            set
            {
                OnUpdated_AtChanging(value);
                ReportPropertyChanging("Updated_At");
                _Updated_At = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Updated_At");
                OnUpdated_AtChanged();
            }
        }
        private global::System.DateTime _Updated_At;
        partial void OnUpdated_AtChanging(global::System.DateTime value);
        partial void OnUpdated_AtChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> Created_By
        {
            get
            {
                return _Created_By;
            }
            set
            {
                OnCreated_ByChanging(value);
                ReportPropertyChanging("Created_By");
                _Created_By = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Created_By");
                OnCreated_ByChanged();
            }
        }
        private Nullable<global::System.Int64> _Created_By;
        partial void OnCreated_ByChanging(Nullable<global::System.Int64> value);
        partial void OnCreated_ByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> Updated_By
        {
            get
            {
                return _Updated_By;
            }
            set
            {
                OnUpdated_ByChanging(value);
                ReportPropertyChanging("Updated_By");
                _Updated_By = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Updated_By");
                OnUpdated_ByChanged();
            }
        }
        private Nullable<global::System.Int64> _Updated_By;
        partial void OnUpdated_ByChanging(Nullable<global::System.Int64> value);
        partial void OnUpdated_ByChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Acig_Help_DeskModel", "FK_Sub_Categories_Categories", "Sub_Categories")]
        public EntityCollection<Sub_Categories> Sub_Categories
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Sub_Categories>("Acig_Help_DeskModel.FK_Sub_Categories_Categories", "Sub_Categories");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Sub_Categories>("Acig_Help_DeskModel.FK_Sub_Categories_Categories", "Sub_Categories", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Acig_Help_DeskModel", Name="Sub_Categories")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Sub_Categories : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Sub_Categories object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="category_Id">Initial value of the Category_Id property.</param>
        /// <param name="created_At">Initial value of the Created_At property.</param>
        /// <param name="updated_At">Initial value of the Updated_At property.</param>
        public static Sub_Categories CreateSub_Categories(global::System.Int64 id, global::System.String name, global::System.Int64 category_Id, global::System.DateTime created_At, global::System.DateTime updated_At)
        {
            Sub_Categories sub_Categories = new Sub_Categories();
            sub_Categories.Id = id;
            sub_Categories.Name = name;
            sub_Categories.Category_Id = category_Id;
            sub_Categories.Created_At = created_At;
            sub_Categories.Updated_At = updated_At;
            return sub_Categories;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int64 _Id;
        partial void OnIdChanging(global::System.Int64 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 Category_Id
        {
            get
            {
                return _Category_Id;
            }
            set
            {
                OnCategory_IdChanging(value);
                ReportPropertyChanging("Category_Id");
                _Category_Id = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Category_Id");
                OnCategory_IdChanged();
            }
        }
        private global::System.Int64 _Category_Id;
        partial void OnCategory_IdChanging(global::System.Int64 value);
        partial void OnCategory_IdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Created_At
        {
            get
            {
                return _Created_At;
            }
            set
            {
                OnCreated_AtChanging(value);
                ReportPropertyChanging("Created_At");
                _Created_At = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Created_At");
                OnCreated_AtChanged();
            }
        }
        private global::System.DateTime _Created_At;
        partial void OnCreated_AtChanging(global::System.DateTime value);
        partial void OnCreated_AtChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Updated_At
        {
            get
            {
                return _Updated_At;
            }
            set
            {
                OnUpdated_AtChanging(value);
                ReportPropertyChanging("Updated_At");
                _Updated_At = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Updated_At");
                OnUpdated_AtChanged();
            }
        }
        private global::System.DateTime _Updated_At;
        partial void OnUpdated_AtChanging(global::System.DateTime value);
        partial void OnUpdated_AtChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> Created_By
        {
            get
            {
                return _Created_By;
            }
            set
            {
                OnCreated_ByChanging(value);
                ReportPropertyChanging("Created_By");
                _Created_By = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Created_By");
                OnCreated_ByChanged();
            }
        }
        private Nullable<global::System.Int64> _Created_By;
        partial void OnCreated_ByChanging(Nullable<global::System.Int64> value);
        partial void OnCreated_ByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> Updated_By
        {
            get
            {
                return _Updated_By;
            }
            set
            {
                OnUpdated_ByChanging(value);
                ReportPropertyChanging("Updated_By");
                _Updated_By = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Updated_By");
                OnUpdated_ByChanged();
            }
        }
        private Nullable<global::System.Int64> _Updated_By;
        partial void OnUpdated_ByChanging(Nullable<global::System.Int64> value);
        partial void OnUpdated_ByChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Acig_Help_DeskModel", "FK_Sub_Categories_Categories", "Category")]
        public Category Category
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Acig_Help_DeskModel.FK_Sub_Categories_Categories", "Category").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Acig_Help_DeskModel.FK_Sub_Categories_Categories", "Category").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Category> CategoryReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Acig_Help_DeskModel.FK_Sub_Categories_Categories", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Category>("Acig_Help_DeskModel.FK_Sub_Categories_Categories", "Category", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
