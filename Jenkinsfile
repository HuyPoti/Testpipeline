pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
        BUILD_CONFIGURATION = 'Release'
    }

    stages {
        stage('Restore') {
            steps {
                echo 'Restoring dependencies...'
                script {
                    if (isUnix()) {
                        sh 'dotnet restore'
                    } else {
                        bat 'dotnet restore'
                    }
                }
            }
        }

        stage('Build') {
            steps {
                echo 'Building the application...'
                script {
                    if (isUnix()) {
                        sh "dotnet build --configuration ${env.BUILD_CONFIGURATION} --no-restore"
                    } else {
                        bat "dotnet build --configuration ${env.BUILD_CONFIGURATION} --no-restore"
                    }
                }
            }
        }

        stage('Test') {
            steps {
                echo 'Running unit tests...'
                script {
                    // SimpleApp currently only has one project, test it directly 
                    if (isUnix()) {
                        sh "dotnet test --configuration ${env.BUILD_CONFIGURATION} --no-build --verbosity normal"
                    } else {
                        bat "dotnet test --configuration ${env.BUILD_CONFIGURATION} --no-build --verbosity normal"
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
