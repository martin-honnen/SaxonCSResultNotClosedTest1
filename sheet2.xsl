<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:saxon="http://saxon.sf.net/"
  expand-text="yes"
  exclude-result-prefixes="#all">

	<xsl:param name="input-uri" as="xs:string" required="yes"/>

	<xsl:param name="OS" as="xs:string" required="yes"/>

	<xsl:mode on-no-match="shallow-copy"/>

	<xsl:template name="xsl:initial-template">
		<xsl:source-document href="{$input-uri}">
			<test>Run with {system-property('xsl:product-name')} {system-property('xsl:product-version')} on {$OS} at {saxon:timestamp()}</test>
			<xsl:result-document href="result2.xml">
				<xsl:apply-templates select="."/>
			</xsl:result-document>
		</xsl:source-document>
	</xsl:template>

	<xsl:template match="/">
		<xsl:next-match/>
		<xsl:comment>Run with {system-property('xsl:product-name')} {system-property('xsl:product-version')} on {$OS} at {saxon:timestamp()}</xsl:comment>
	</xsl:template>

</xsl:stylesheet>
