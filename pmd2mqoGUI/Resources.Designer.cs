﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18444
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pmd2mqoGUI {
	using System;
	
	
	/// <summary>
	///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
	/// </summary>
	// Этот класс создан автоматически классом StronglyTypedResourceBuilder
	// с помощью такого средства, как ResGen или Visual Studio.
	// Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
	// с параметром /str или перестройте свой проект VS.
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	internal class Resources {
		
		private static global::System.Resources.ResourceManager resourceMan;
		
		private static global::System.Globalization.CultureInfo resourceCulture;
		
		[global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Resources() {
		}
		
		/// <summary>
		///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager {
			get {
				if (object.ReferenceEquals(resourceMan, null)) {
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("pmd2mqoGUI.Resources", typeof(Resources).Assembly);
					resourceMan = temp;
				}
				return resourceMan;
			}
		}
		
		/// <summary>
		///   Перезаписывает свойство CurrentUICulture текущего потока для всех
		///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Globalization.CultureInfo Culture {
			get {
				return resourceCulture;
			}
			set {
				resourceCulture = value;
			}
		}
		
		/// <summary>
		///   Поиск локализованного ресурса типа System.Drawing.Icon, аналогичного (Значок).
		/// </summary>
		internal static System.Drawing.Icon icon {
			get {
				object obj = ResourceManager.GetObject("icon", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}
		
		/// <summary>
		///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
		/// </summary>
		internal static System.Drawing.Bitmap iconPNG {
			get {
				object obj = ResourceManager.GetObject("iconPNG", resourceCulture);
				return ((System.Drawing.Bitmap)(obj));
			}
		}
		
		/// <summary>
		///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
		/// </summary>
		internal static System.Drawing.Bitmap process {
			get {
				object obj = ResourceManager.GetObject("process", resourceCulture);
				return ((System.Drawing.Bitmap)(obj));
			}
		}
	}
}
