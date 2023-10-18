pipeline {
    agent any
    stages {
        stage('Checkout code') {
            steps {
                echo 'Starting to build the App.'
                checkout scm
            }
        }
        stage('Run My Test') {
            steps {
                echo 'Run My Test'
                sh 'pwd'
                sh 'dotnet test'
            }
        }
        stage('Retrieve logs') {
            steps {
                script {
                    def date = new Date().format("yyyyMMdd")
                    sh "cat /var/lib/jenkins/workspace/Pipeline_Nour/bin/Debug/reportResult/Logs/LogReport.logLogs/LogReport_${date}.log"
                    sh "cp /var/lib/jenkins/workspace/Pipeline_Nour/bin/Debug/reportResult/Logs/LogReport.logLogs/LogReport_${date}.log ./"
                    archiveArtifacts artifacts: "LogReport_${date}.log", onlyIfSuccessful: false
                }
            }
        }
    }
}
