using System;
using System.Linq;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyVersion("0.1.0.0")]
[assembly: AssemblyFileVersion("0.1.0.0")]
#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("David Sidlinger")]
[assembly: AssemblyProduct("Saccharin")]
[assembly: AssemblyCopyright("Copyright © David Sidlinger 2010")]
