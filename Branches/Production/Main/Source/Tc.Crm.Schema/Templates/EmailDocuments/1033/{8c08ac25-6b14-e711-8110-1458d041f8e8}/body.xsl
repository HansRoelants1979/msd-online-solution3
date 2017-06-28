﻿<?xml version="1.0" ?><xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"><xsl:output method="text" indent="no"/><xsl:template match="/data"><![CDATA[<font face=Tahoma size=2><span style="color:rgb(51, 51, 51);">Beste</span>&#160;]]><xsl:choose><xsl:when test="contact/tc_salutation/@name"><xsl:value-of select="contact/tc_salutation/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ &#160;]]><xsl:choose><xsl:when test="contact/lastname"><xsl:value-of select="contact/lastname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ ]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="color:rgb(51, 51, 51);"> </span></font><div><font face=Tahoma size=2><font color="#333333"><br></font></font><div><font face=Tahoma size=2><span style="color:rgb(51, 51, 51);">Klant</span><span style="color:rgb(51, 51, 51);">: &#160;]]><xsl:choose><xsl:when test="contact/fullname"><xsl:value-of select="contact/fullname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ </span>]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="color:rgb(51, 51, 51);"> </span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Boekingsnummer</span><span style="color:rgb(51, 51, 51);">: &#160;</span>]]><xsl:choose><xsl:when test="incident/tc_bookingreferencefreetext"><xsl:value-of select="incident/tc_bookingreferencefreetext" /></xsl:when><xsl:when test="incident/tc_bookingid/@name"><xsl:value-of select="incident/tc_bookingid/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="white-space:nowrap;"> </span><br style="color:rgb(51, 51, 51);"><br style="color:rgb(51, 51, 51);"></font><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>Bedankt voor de feedback.&#160;&#160;&#160;&#160;&#160;<br><br>U zei dat:&#160;]]><xsl:choose><xsl:when test="incident/description"><xsl:value-of select="incident/description" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ <br><br>We onderzoeken uw schrijven en brengen u op de hoogte van zodra het onderzoek is afgerond. Heeft u nog verdere vragen? Aarzel niet ons te contateren.<br><br>Alvast bedankt voor uw geduld tijdens de behandeling van uw bemerkingen.</font><font face=Tahoma size=2><br></font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><img src="http://cdn.thomascook.com/crm/email/logo/BE-logos-ThomasCookNeckermannPegase.png"><font face=Tahoma size=2><br></font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>TCRB n.v./s.a. (Lic. (A) 1067), Tramstraat 67C, B-9052 Gent, Belgium, BTW/TVA BE 0412.677.887 RPR/RPM Gent, IBAN BE59 7330 3834 3726 (BIC KREDBEBB) handelt in naam en voor rekening van/ agit au nom et pour compte de (i) THCB n.v/s.a.(Lic. (A) 1457), Tramstraat 63-67, B-9052 Gent, Belgium, BTW/TVA BE 0418.052.479 RPR/RPM Gent voor vliegvakanties/pour vacances en avion of/ ou (ii) Thomas Cook International a.g (Lic. A 5880), Poststrasse 4, CH-8808 Pfäffikon, Switzerland, met bijkantoor/ayant une succursale 54 bd de la Sauvenière, B-4000 Liège, Belgium, ondernemingsnr./n° d’entreprise 533.973.122 voor autovakanties &amp; citytrips/pour vacances en voiture &amp; citytrips</font></p></div></div><font face="Tahoma, Verdana, Arial" size=2 style="display:inline;"></font>]]></xsl:template></xsl:stylesheet>