﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmatWinFormsNetFramework1402.Relatorios.Escolas.Sorocaba {
    using System;
    using System.ComponentModel;
    using CrystalDecisions.Shared;
    using CrystalDecisions.ReportSource;
    using CrystalDecisions.CrystalReports.Engine;
    
    
    public class rptRequerimentoMatricula : ReportClass {
        
        public rptRequerimentoMatricula() {
        }
        
        public override string ResourceName {
            get {
                return "rptRequerimentoMatricula.rpt";
            }
            set {
                // Do nothing
            }
        }
        
        public override bool NewGenerator {
            get {
                return true;
            }
            set {
                // Do nothing
            }
        }
        
        public override string FullResourceName {
            get {
                return "EmatWinForms1402.Relatorios.Escolas.Sorocaba.rptRequerimentoMatricula.rpt";
            }
            set {
                // Do nothing
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.CrystalReports.Engine.Section Section1 {
            get {
                return this.ReportDefinition.Sections[0];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.CrystalReports.Engine.Section Section2 {
            get {
                return this.ReportDefinition.Sections[1];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.CrystalReports.Engine.Section Section3 {
            get {
                return this.ReportDefinition.Sections[2];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.CrystalReports.Engine.Section Section4 {
            get {
                return this.ReportDefinition.Sections[3];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.CrystalReports.Engine.Section Section5 {
            get {
                return this.ReportDefinition.Sections[4];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_nMatricula {
            get {
                return this.DataDefinition.ParameterFields[0];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_nome {
            get {
                return this.DataDefinition.ParameterFields[1];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_rg {
            get {
                return this.DataDefinition.ParameterFields[2];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_ensino {
            get {
                return this.DataDefinition.ParameterFields[3];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_ufRg {
            get {
                return this.DataDefinition.ParameterFields[4];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_ra {
            get {
                return this.DataDefinition.ParameterFields[5];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_dtMatricula {
            get {
                return this.DataDefinition.ParameterFields[6];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_dtNascimento {
            get {
                return this.DataDefinition.ParameterFields[7];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_localNascimento {
            get {
                return this.DataDefinition.ParameterFields[8];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_ufNascimento {
            get {
                return this.DataDefinition.ParameterFields[9];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_paisNascimento {
            get {
                return this.DataDefinition.ParameterFields[10];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_cor {
            get {
                return this.DataDefinition.ParameterFields[11];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_logradouro {
            get {
                return this.DataDefinition.ParameterFields[12];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_numero {
            get {
                return this.DataDefinition.ParameterFields[13];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_bairro {
            get {
                return this.DataDefinition.ParameterFields[14];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_cidade {
            get {
                return this.DataDefinition.ParameterFields[15];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_uf {
            get {
                return this.DataDefinition.ParameterFields[16];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_cep {
            get {
                return this.DataDefinition.ParameterFields[17];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_termo {
            get {
                return this.DataDefinition.ParameterFields[18];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_sexo {
            get {
                return this.DataDefinition.ParameterFields[19];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_mae {
            get {
                return this.DataDefinition.ParameterFields[20];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_estadoCivil {
            get {
                return this.DataDefinition.ParameterFields[21];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_dataRelatorio {
            get {
                return this.DataDefinition.ParameterFields[22];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_contato {
            get {
                return this.DataDefinition.ParameterFields[23];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_usuario {
            get {
                return this.DataDefinition.ParameterFields[24];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_dataMatricula {
            get {
                return this.DataDefinition.ParameterFields[25];
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public CrystalDecisions.Shared.IParameterField Parameter_complemento {
            get {
                return this.DataDefinition.ParameterFields[26];
            }
        }
    }
    
    [System.Drawing.ToolboxBitmapAttribute(typeof(CrystalDecisions.Shared.ExportOptions), "report.bmp")]
    public class CachedrptRequerimentoMatricula : Component, ICachedReport {
        
        public CachedrptRequerimentoMatricula() {
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool IsCacheable {
            get {
                return true;
            }
            set {
                // 
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool ShareDBLogonInfo {
            get {
                return false;
            }
            set {
                // 
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual System.TimeSpan CacheTimeOut {
            get {
                return CachedReportConstants.DEFAULT_TIMEOUT;
            }
            set {
                // 
            }
        }
        
        public virtual CrystalDecisions.CrystalReports.Engine.ReportDocument CreateReport() {
            rptRequerimentoMatricula rpt = new rptRequerimentoMatricula();
            rpt.Site = this.Site;
            return rpt;
        }
        
        public virtual string GetCustomizedCacheKey(RequestContext request) {
            String key = null;
            // // The following is the code used to generate the default
            // // cache key for caching report jobs in the ASP.NET Cache.
            // // Feel free to modify this code to suit your needs.
            // // Returning key == null causes the default cache key to
            // // be generated.
            // 
            // key = RequestContext.BuildCompleteCacheKey(
            //     request,
            //     null,       // sReportFilename
            //     this.GetType(),
            //     this.ShareDBLogonInfo );
            return key;
        }
    }
}
