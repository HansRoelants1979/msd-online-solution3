﻿<?xml version="1.0" ?><xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"><xsl:output method="text" indent="no"/><xsl:template match="/data"><![CDATA[<span style="font-family:Tahoma;font-size:small;">Dear&#160;</span>]]><xsl:choose><xsl:when test="contact/tc_salutation/@name"><xsl:value-of select="contact/tc_salutation/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="font-family:Tahoma;font-size:small;"> </span>]]><xsl:choose><xsl:when test="contact/lastname"><xsl:value-of select="contact/lastname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="font-family:Tahoma;font-size:small;"> </span>]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="font-family:Tahoma;font-size:small;color:rgb(51, 51, 51);"> </span><br><div><font color="#333333" face=Tahoma size=2><br></font><font face=Tahoma size=2><font style="display:inline;"></font></font><div><font face=Tahoma size=2><span style="color:rgb(51, 51, 51);">Customer: &#160;]]><xsl:choose><xsl:when test="contact/fullname"><xsl:value-of select="contact/fullname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ </span>]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="color:rgb(51, 51, 51);"> </span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Booking Reference: &#160;</span>]]><xsl:choose><xsl:when test="incident/tc_bookingreferencefreetext"><xsl:value-of select="incident/tc_bookingreferencefreetext" /></xsl:when><xsl:when test="incident/tc_bookingid/@name"><xsl:value-of select="incident/tc_bookingid/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="white-space:nowrap;"> </span><br style="color:rgb(51, 51, 51);"><br style="color:rgb(51, 51, 51);"></font><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>Following our conversation, this email is to confirm that your case is now closed.&#160;&#160;&#160;&#160;&#160;&#160;<br><br>Thanks again for taking the time to tell us about your experience. We're sorry you had to contact us. Your feedback really is important to us and we hope you'll book with us again in the future.<br></font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><img src="http://cdn.thomascook.com/crm/email/logo/Thomas_Cook_Cruise.png" style="font-family:Tahoma;font-size:small;"><br style="font-family:Tahoma;font-size:small;"><span style="font-family:Tahoma;font-size:small;">Customer Relations</span><br style="font-family:Tahoma;font-size:small;"><span style="font-family:Tahoma;font-size:small;">Tel No: 0844 879 8135</span><br style="font-family:Tahoma;font-size:small;"><span style="font-family:Tahoma;font-size:small;">Opening Hours: 9am-8pm Mon-Fri, 9am-5pm Sat.</span><br style="font-family:Tahoma;font-size:small;"><br style="font-family:Tahoma;font-size:small;"><span style="font-family:Tahoma;font-size:small;">Thomas Cook Cruise&#160;is a trading name of TCCT Retail Limited.</span><br style="font-family:Tahoma;font-size:small;"><br style="font-family:Tahoma;font-size:small;"><span style="font-family:Tahoma;font-size:small;">Registered Office: The Thomas Cook Business Park, Coningsby Road, Peterborough, PE3 8SB</span><br style="font-family:Tahoma;font-size:small;"><span style="font-family:Tahoma;font-size:small;">Registered in England No. 7397858</span><br></p></div></div><font face="Tahoma, Verdana, Arial" size=2 style="display:inline;"></font>]]></xsl:template></xsl:stylesheet>