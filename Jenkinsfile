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
            steps {
                echo 'Restoring projects...'
                sh 'dotnet restore SimpleApp.Tests/SimpleApp.Tests.csproj'
            }
        }

        stage('Build') {
            steps {
                echo 'Building the application...'
                script {
                    def buildCmd = "dotnet build SimpleApp.Tests/SimpleApp.Tests.csproj --configuration ${env.BUILD_CONFIGURATION} --no-restore"
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
                    def testCmd = "dotnet test SimpleApp.Tests/SimpleApp.Tests.csproj --configuration ${env.BUILD_CONFIGURATION} --no-build --verbosity normal"
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
                    // Specify project file to avoid confusion with test project
                    def publishCmd = "dotnet publish SimpleApp.csproj --configuration ${env.BUILD_CONFIGURATION} --no-build --output ./publish"
                    if (isUnix()) {
                        sh publishCmd
                    } else {
                        bat publishCmd
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
