﻿<?xml version="1.0" ?><xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"><xsl:output method="text" indent="no"/><xsl:template match="/data"><![CDATA[<font face=Tahoma size=2><span style="color:rgb(51, 51, 51);">Monsieur / Madame&#160;</span>&#160;]]><xsl:choose><xsl:when test="contact/lastname"><xsl:value-of select="contact/lastname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ ]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="color:rgb(51, 51, 51);"> </span></font><div><font face=Tahoma size=2><font color="#333333"><br></font></font><div><font face=Tahoma size=2><span style="color:rgb(51, 51, 51);">Client:</span><span style="color:rgb(51, 51, 51);">&#160;&#160;]]><xsl:choose><xsl:when test="contact/fullname"><xsl:value-of select="contact/fullname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ </span>]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="color:rgb(51, 51, 51);"> </span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Numéro de réservation:</span><span style="color:rgb(51, 51, 51);">&#160;&#160;</span>]]><xsl:choose><xsl:when test="incident/tc_bookingreferencefreetext"><xsl:value-of select="incident/tc_bookingreferencefreetext" /></xsl:when><xsl:when test="incident/tc_bookingid/@name"><xsl:value-of select="incident/tc_bookingid/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="white-space:nowrap;"> </span><br style="color:rgb(51, 51, 51);"><br style="color:rgb(51, 51, 51);"></font><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>C'est avec attention que nous avons pris connaissance de vos remarques:&#160;]]><xsl:choose><xsl:when test="incident/description"><xsl:value-of select="incident/description" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ <br><br>Nous effectuons actuellement les démarches nécessaires afin de revenir rapidement vers vous. Dans l'intervalle, n'hésitez pas à nous contacter pour toutes informations ou demandes complémentaires.<br>En vous remerciant de votre compréhension.</font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2><img style="" src="http://cdn.thomascook.com/images/uploads/icons/FR%20-%20Jettours_pantone.jpg"><br></font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>Thomas Cook SAS- Siège Social : 92/98 Boulevard Victor Hugo - 92 115 Clichy Cedex<br>S.A.S au capital de 10.000.000 euros - RCS Nanterre B 572 158 905<br>Numéro d’immatriculation au Registre des Opérateurs de Voyages et de Séjours : IM092100061<br>Garantie Financière : APST – 15 avenue Carnot – 75017 Paris<br>Assurances RCP : AXA Corporate Solutions Assurance - 4, rue Jules Lefebvre - 75426 Paris Cedex 09<br>N° de TVA intra-communautaire : FR 35 572 158 905</font><font face=Tahoma size=2><br></font></p></div><font face="Tahoma, Verdana, Arial" size=2 style="display:inline;"></font></div>]]></xsl:template></xsl:stylesheet>