﻿<?xml version="1.0" ?><xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"><xsl:output method="text" indent="no"/><xsl:template match="/data"><![CDATA[Pegase - plus d’informations concernant votre dossier: &#160;]]><xsl:choose><xsl:when test="incident/tc_bookingreferencefreetext"><xsl:value-of select="incident/tc_bookingreferencefreetext" /></xsl:when><xsl:when test="incident/tc_bookingid/@name"><xsl:value-of select="incident/tc_bookingid/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ ]]></xsl:template></xsl:stylesheet>