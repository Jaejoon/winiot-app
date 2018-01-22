﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace IOTOIApp.Utils
{
    public class UIElementUtil
    {
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;

                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }

                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static IEnumerable<T> FindChildArray<T>(DependencyObject depObj, string childName) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    var frameworkElement = child as FrameworkElement;

                    if (child != null && child is T && frameworkElement.Name == childName)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindChildArray<T>(child, childName))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null) return null;
            T foundParent = null;

            //for (int i = 0; i < 10; i++)
            //{
                var parent = VisualTreeHelper.GetParent(child);
                T parentType = parent as T;

                if (parentType == null)
                {
                    foundParent = FindParent<T>(parent);
                    //if (foundParent != null) break;
                }
                else
                {
                    foundParent = (T)parent;
                    //break;
                }
            //}

            return foundParent;
        }
    }
}
