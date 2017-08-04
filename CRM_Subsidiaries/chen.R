library(rvest)
library(sqldf)
library(stringr)
library(httr)
library(dplyr)
mydata <- data.frame()
fiveHFortune<- read.csv(file="wiki.csv",header=TRUE, sep=",")
x = NULL
y = NULL
cyberResilience = NULL
for(company in fiveHFortune[,1]){
  company_name<- as.character(company)
  #Baker Hughes
  x = append(x,company_name)
  str_URL <- paste("https://en.wikipedia.org/wiki/",company_name,sep="")
  url <- str_URL
  #print(url)
  try(
  temp <- url %>% 
    
    read_html %>%
    html_nodes("table")
  )
  wikiTable <- html_table(temp[1],fill=TRUE)
  #colnames(wikiTable)
  status<-toString(wikiTable)
  if (status == "") {
    subsidiaryCount= -1
    #mydata <- rbind(mydata,c(company_name, as.character(subsidiaryCount)))
    y=append(y,subsidiaryCount)
  } else {
    rowNumber<-which(grepl("Subsidiaries", wikiTable[[1]][,1]))
    #grepl("Subsidiaries", wikiTable[[1]][,1])
    #check if it has subsidiary
      if(toString(rowNumber) != ""){
        subsidiaryData<-wikiTable[[1]][rowNumber,2]
        #get the numbers of subsidiaries
        if(subsidiaryData == "List of subsidiaries"){
          subsidiaryCount= -99
          #mydata <- rbind(mydata,c(company_name, as.character(subsidiaryCount)))
          y=append(y,subsidiaryCount)
        }
        else{
          symbolComma<-grepl("\n",subsidiaryData)
          #symbolComma
          ifelse(symbolComma == "FALSE", subsidiaryCount <- str_count(subsidiaryData, " , "), subsidiaryCount <- str_count(subsidiaryData, "\n")
          )
          #subsidiaryCount
          y=append(y,subsidiaryCount)
        }
      }
      else{
        subsidiaryCount<- 0
        #mydata <- rbind(mydata,c(company_name, as.character(subsidiaryCount)))
        y=append(y,subsidiaryCount)
      }
    }

  if(subsidiaryCount>=0 && subsidiaryCount<3){
    cyberResilience = append(cyberResilience, 10)
  }
  else if(subsidiaryCount>=3 && subsidiaryCount<6){
    cyberResilience = append(cyberResilience, 9)
  }
  else if(subsidiaryCount>=6 && subsidiaryCount<10){
    cyberResilience = append(cyberResilience, 8)
  }
  else if(subsidiaryCount>=10 && subsidiaryCount<15){
    cyberResilience = append(cyberResilience, 7)
  }
  else if(subsidiaryCount>=15 && subsidiaryCount<21){
    cyberResilience = append(cyberResilience, 6)
  }
  else if(subsidiaryCount>=21 && subsidiaryCount<28){
    cyberResilience = append(cyberResilience, 5)
  }
  else if(subsidiaryCount>=28 && subsidiaryCount<36){
    cyberResilience = append(cyberResilience, 4)
  }
  else if(subsidiaryCount>=36 && subsidiaryCount<45){
    cyberResilience = append(cyberResilience, 3)
  }
  else if(subsidiaryCount>=45 && subsidiaryCount<55){
    cyberResilience = append(cyberResilience, 2)
  }
  else if(subsidiaryCount>=56){
    cyberResilience = append(cyberResilience, 1)
  }
  else{
    cyberResilience = append(cyberResilience, -1)
  }
}
mydata = data.frame(x, y,cyberResilience)
colnames(mydata)[colnames(mydata) == 'x'] <- 'CompanyName'
colnames(mydata)[colnames(mydata) == 'y'] <- 'SubsidiariesName'

library(plyr)
mydata<- arrange(mydata,desc(SubsidiariesName),CompanyName)
View(mydata)
#write CSV in R
write.csv(mydata, file = "SubsidiaryCyber.csv")

#connect to sql server
#library(RODBC)

#myConn <- odbcDriverConnect(connection="Driver={SQL Server Management studio}; server=TCP:79.30.2.148,1433; Database=CRM_Subsidiaries; User Id=sa; Password=sa#1234;trusted_connection=yes;")


