library(rvest)
library(sqldf)
library(stringr)
library(httr)
mydata <- data.frame()
fiveHFortune<- read.csv(file="wiki.csv",header=TRUE, sep=",")
for(company in fiveHFortune[,1]){
  company_name<- as.character(company)
  #Baker Hughes
  str_URL <- paste("https://en.wikipedia.org/wiki/",company_name,sep="")
  url <- str_URL
  print(url)
  try(
  temp <- url %>% 
    
    read_html %>%
    html_nodes("table")
  )
  wikiTable <- html_table(temp[1],fill=FALSE)
  #colnames(wikiTable)
  rowNumber<-which(grepl("Subsidiaries", wikiTable[[1]][,1]))
  #grepl("Subsidiaries", wikiTable[[1]][,1])
  #check if it has subsidiary
  subsidiaryData<-wikiTable[[1]][rowNumber,2]
  status<-toString(subsidiaryData)
  if (status == "") {
    subsidiaryCount<-0
    mydata <- rbind(mydata,c(company_name, as.character(subsidiaryCount)))
  } else {
    #get the numbers of subsidiaries
    symbolComma<-grepl("\n",subsidiaryData)
    #symbolComma
    ifelse(symbolComma == "FALSE", subsidiaryCount <- str_count(subsidiaryData, " , "), subsidiaryCount <- str_count(subsidiaryData, "\n")
    )
    #subsidiaryCount
    mydata <- rbind(mydata,data.frame(company_name, subsidiaryCount))
  }
  
}
colnames(mydata)[colnames(mydata) == 'x'] <- 'CompanyName'
colnames(mydata)[colnames(mydata) == 'y'] <- 'SubsidiariesName'
View(mydata)

