pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
        BUILD_CONFIGURATION = 'Release'
        DOTNET_PRINT_TELEMETRY_MESSAGE = 'false'
        DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER = '0'
    }

    stages {
        stage('Restore') {
            environment {
        // Ép các công cụ sử dụng IPv4 khi có thể
        DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER = "0" 
        }
        steps {
            sh 'export DOTNET_PRINT_TELEMETRY_MESSAGE=false'
            sh 'dotnet restore SimpleApp.slnx'
        }
        }

        stage('Build') {
            steps {
                echo 'Building the application...'
                script {
                    def buildCmd = "dotnet build SimpleApp.slnx --configuration ${env.BUILD_CONFIGURATION} --no-restore"
                    if (isUnix()) {
                        sh buildCmd
                    } else {
                        bat buildCmd
                    }
                }
            }
        }

        stage('Test') {
            steps {
                echo 'Running unit tests...'
                script {
                    def testCmd = "dotnet test SimpleApp.slnx --configuration ${env.BUILD_CONFIGURATION} --no-build --verbosity normal"
                    if (isUnix()) {
                        sh testCmd
                    } else {
                        bat testCmd
                    }
                }
            }
        }

        stage('Publish') {
            steps {
                echo 'Publishing the application...'
                script {
                    if (isUnix()) {
                        sh "dotnet publish SimpleApp.csproj --configuration ${env.BUILD_CONFIGURATION} --no-build --output ./publish"
                    } else {
                        bat "dotnet publish SimpleApp.csproj --configuration ${env.BUILD_CONFIGURATION} --no-build --output ./publish"
                    }
                }
            }
        }
        
        stage('Archive Artifacts') {
            steps {
                echo 'Archiving build artifacts...'
                archiveArtifacts artifacts: 'publish/**', fingerprint: true
            }
        }
    }

    post {
        always {
            echo 'Pipeline execution finished.'
        }
        success {
            echo 'Build succeeded!'
        }
        failure {
            echo 'Build failed!'
        }
    }
}
