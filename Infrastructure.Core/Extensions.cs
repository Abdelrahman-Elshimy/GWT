﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Core
{
    /// <summary>
    /// Refer to http://www.albahari.com/nutshell/linqkit.html and http://tomasp.net/blog/linq-expand.aspx for more information.
    /// </summary>
    public static class Extensions
    {
        public static List<T> ToGeneric<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        /// <summary>
        /// LinqKit: Returns wrapper that automatically expands expressions using a default QueryOptimizer.
        /// </summary>
        public static IQueryable<T> AsExpandable<T>(this IQueryable<T> query)
        {
            return AsExpandable(query, LinqKitExtension.QueryOptimizer);
        }

        /// <summary>
        /// LinqKit: Returns wrapper that automatically expands expressions using a custom QueryOptimizer.
        /// </summary>
        public static IQueryable<T> AsExpandable<T>(this IQueryable<T> query, Func<Expression, Expression> queryOptimizer)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (queryOptimizer == null)
            {
                throw new ArgumentNullException(nameof(queryOptimizer));
            }

            if (query is ExpandableQuery<T>)
            {
                return query;
            }

            return new ExpandableQuery<T>(query, queryOptimizer);
        }

        /// <summary> LinqKit: Expands expression </summary>
        public static Expression<TDelegate> Expand<TDelegate>(this Expression<TDelegate> expr)
        {
            return (Expression<TDelegate>)new ExpressionExpander().Visit(expr);
        }

        /// <summary> LinqKit: Expands expression </summary>
        public static Expression Expand<TDelegate>(this ExpressionStarter<TDelegate> expr)
        {
            return expr != null && (expr.IsStarted || expr.UseDefaultExpression) ? new ExpressionExpander().Visit(expr) : null;
        }

        /// <summary> LinqKit: Expands expression </summary>
        public static Expression Expand(this Expression expr)
        {
            return new ExpressionExpander().Visit(expr);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<TResult>(this Expression<Func<TResult>> expr)
        {
            return expr.Compile().Invoke();
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, TResult>(this Expression<Func<T1, TResult>> expr, T1 arg1)
        {
            return expr.Compile().Invoke(arg1);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> expr, T1 arg1, T2 arg2)
        {
            return expr.Compile().Invoke(arg1, arg2);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> expr, T1 arg1, T2 arg2, T3 arg3)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4);
        }

#if !(NET35 || NET40)
        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        /// <summary> LinqKit: Compile and invoke </summary>
        public static TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            return expr.Compile().Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        }
#endif
    }
}
