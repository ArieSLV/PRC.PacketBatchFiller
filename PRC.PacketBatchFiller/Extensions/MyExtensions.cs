using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catel.Data;
using Catel.MVVM;
using PhoneNumbers;
using PRC.PacketBatchFiller;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using Xceed.Wpf.Toolkit;


public static class MyExtensions
{
    public static object GetIDPropertyValue<T>(this T obj)
    {
        var t = typeof (T);
        if (t.BaseType?.Name == "ModelBase") return t.GetProperty($"{t.Name}Id").GetValue(obj, null);
        else
        {
            return t.GetProperty($"{t.BaseType.Name}Id").GetValue(obj, null);
        }
    }

    public static void SetMainPropertyValue<T>(this T obj, object valueToSet)
    {
        var t = typeof (T);
        t.GetProperty(t.GetField("MainField").GetValue(obj).ToString()).SetValue(obj, valueToSet);
    }

    public static object GetFieldValue<T>(this T obj, string name)
    {
        var t = typeof (T);
        return t.GetField(name).GetValue(obj);
    }

    public static object GetMainPropertyName<T>(this T obj)
    {
        try
        {
            var t = typeof (T);
            return t.GetField("MainField").GetValue(obj);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Invalid Entity Type supplied for Lookup", ex);
        }
    }

    public static object GetEntityMainPropertyValue<T>(this T obj)
    {
        var t = typeof (T);
        return obj == null
            ? default(object)
            : t.GetProperty(t.GetField("MainField").GetValue(obj).ToString()).GetValue(obj, null);
    }

    public static object GetEntityDefaultValue<T>(this T obj)
    {
        var t = typeof (T);
        if (obj == null) return default(object);

        return t.GetField("DefaultValue").GetValue(obj);
    }

    public static T GetDefaultCollectionItem<T>(this ObservableCollection<T> obj)
    {
        return obj.SingleOrDefault(o =>
        {
            if (o.GetEntityMainPropertyValue() == null || o.GetFieldValue("DefaultValue") == null) return false;

            var defaultEntityValue = o.GetEntityMainPropertyValue().ToString();
            var defaultFieldValue = o.GetFieldValue("DefaultValue").ToString();
            return defaultEntityValue == defaultFieldValue;
        });
    }

    public static DbSet GetDbSet<T>(this PBFContext ctx)
    {
        try
        {
            var dbSet = ctx.Set(typeof (T));
            return dbSet;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Invalid Entity Type supplied for Lookup", ex);
        }
    }

    public static ObservableCollection<T> GetEntityObservableCollection<T>(this PBFContext ctx)
    {
        try
        {
            var key = typeof (T).Name;
            var adapter = (IObjectContextAdapter) ctx;
            var objectContext = adapter.ObjectContext;

            var container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName,
                System.Data.Entity.Core.Metadata.Edm.DataSpace.CSpace);

            var name = container.BaseEntitySets.Where(o => o.ElementType.Name.Equals(key)).FirstOrDefault().Name;

            var query = objectContext.CreateQuery<T>($"[{name}]");


            return new ObservableCollection<T>(query.AsEnumerable());
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Invalid Entity Type supplied for Lookup", ex);
        }
    }
    
    public static AsYouTypeFormatter RemoveLastDigit(this AsYouTypeFormatter asYouTypeFormatter)
    {
        var tmpString = asYouTypeFormatter.InputDigit(' ');
        var tmpaytf = new AsYouTypeFormatter("RU");

        foreach (var character in tmpString.Substring(0, tmpString.Length - 3))
        {
            if (character != ' ' && character != '-') tmpaytf.InputDigit(character);
        }


        return tmpaytf;
    }

    public static string GetRawText(this MaskedTextBox maskedTextBox)
    {
        var maskedTextProvider = maskedTextBox?.MaskedTextProvider;

        return maskedTextProvider?.ToString(true, false, false, 0, maskedTextProvider.Length) ?? string.Empty;
    }

    public static DependencyObject GetRootControl(this DependencyObject reference)
    {
        var parentDependencyObject = VisualTreeHelper.GetParent(reference);

        DependencyObject rootElement = null;

        while (parentDependencyObject != null)
        {
            rootElement = parentDependencyObject;
            parentDependencyObject = VisualTreeHelper.GetParent(parentDependencyObject);
        }

        return rootElement;
    }

    

    public static IViewModel GetRootIViewModel(this ViewModelBase reference)
    {
        var parentViewModel = reference.ParentViewModel;

        IViewModel rootViewModel = null;

        while (parentViewModel != null)
        {
            rootViewModel = parentViewModel;
            parentViewModel = ((ViewModelBase) parentViewModel).ParentViewModel;
        }

        return rootViewModel;
    }

    public static int GetTabIndexOfRootUsercontrol(this DependencyObject dependencyObject)
    {
        var parentDependencyObject = VisualTreeHelper.GetParent(dependencyObject);

        DependencyObject rootElement = null;

        while (!(parentDependencyObject is UserControl))
        {
            parentDependencyObject = VisualTreeHelper.GetParent(parentDependencyObject);
            rootElement = parentDependencyObject;
        }

        var test = 0;
        var frameworkElement = (FrameworkElement)rootElement;
        if (frameworkElement?.Parent != null) test = ((Control)frameworkElement.Parent).TabIndex;

        return test;
    }

    public static ObservableCollection<Unit> GetAuthorizedUnitsCollection(this ShareholderAccount sha)
    {
        var unitsCollectionForReturn = new ObservableCollection<Unit>();

        using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
        {
            var shads = new ObservableCollection<ShareholderAuthorizesDocument>(dbContextManager.Context.ShareholderAuthorizesDocuments
                .Where(o => o.WhoGivingAuthority.ShareholderAccountId == sha.ShareholderAccountId));
            foreach (var authorizedUnit in shads.SelectMany(shad => shad.AuthorizedUnits))
            {
                unitsCollectionForReturn.Add(authorizedUnit);
            }
        }

        return unitsCollectionForReturn;
    }
}