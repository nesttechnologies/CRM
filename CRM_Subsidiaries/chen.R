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
          subsidiaryCount= 99
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
  #y = 5.488* x + 0.357
  cyberResilience = append(cyberResilience, (5.488/(0.38+0.15*subsidiaryCount)+0.357))
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


