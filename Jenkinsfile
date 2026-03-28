pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
        BUILD_CONFIGURATION = 'Release'
    }

    stages {
        stage('Restore') {
            environment {
        // Ép các công cụ sử dụng IPv4 khi có thể
        DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER = "0" 
        }
        steps {
            // Hoặc dùng lệnh linux để tắt tạm thời ưu tiên IPv6 cho phiên làm việc này
            sh 'export DOTNET_PRINT_TELEMETRY_MESSAGE=false'
            sh 'dotnet restore'
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
