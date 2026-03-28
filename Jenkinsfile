pipeline {
    agent any
    
    environment {
        // Ép dotnet sử dụng IPv4 để tránh lỗi timeout NU1301 đã gặp lúc đầu
        DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER = "0"
        DOTNET_PRINT_TELEMETRY_MESSAGE = "false"
        BUILD_CONFIGURATION = 'Release'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                echo 'Restoring NuGet packages...'
                // Khôi phục tất cả thư viện (bao gồm cả dự án chính thông qua tham chiếu)
                sh 'dotnet restore SimpleApp.Tests/SimpleApp.Tests.csproj'
            }
        }

        stage('Build') {
            steps {
                echo 'Building the application (including tests)...'
                // Build dự án thử nghiệm sẽ tự động build đồng thời cả dự án chính
                sh "dotnet build SimpleApp.Tests/SimpleApp.Tests.csproj --configuration ${env.BUILD_CONFIGURATION} --no-restore"
            }
        }

        stage('Test') {
            steps {
                echo 'Running Unit Tests...'
                // Chạy test sử dụng file đã compile từ bước Build
                sh "dotnet test SimpleApp.Tests/SimpleApp.Tests.csproj --configuration ${env.BUILD_CONFIGURATION} --no-build"
            }
        }

        stage('Publish') {
            steps {
                echo 'Publishing the application...'
                // Xuất bản ứng dụng chính cho việc triển khai
                sh "dotnet publish SimpleApp.csproj --configuration ${env.BUILD_CONFIGURATION} --no-build --output ./publish"
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
            echo 'Build, Test and Publish succeeded!'
        }
        failure {
            echo 'Build failed! Please check the console log.'
        }
    }
}