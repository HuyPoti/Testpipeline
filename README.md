# ASP.NET Core Jenkins Pipeline Test

Dự án mẫu để thực hành thiết lập Pipeline CI/CD với Jenkins cho ASP.NET Core.

## Cấu trúc dự án
- `SimpleApp/`: ASP.NET Core Web API (Project chính).
- `SimpleApp.Tests/`: Unit tests sử dụng xUnit.
- `Jenkinsfile`: Cấu hình Pipeline (Declarative).
- `.gitignore`: Cấu hình bỏ qua các file không cần thiết cho Git.

## Hướng dẫn thiết lập Jenkins Pipeline
1. **Cài đặt .NET SDK**: Đảm bảo trên Jenkins agent đã được cài đặt .NET 8 (theo phiên bản của dự án này).
2. **Tạo Pipeline Job**:
   - Trên Jenkins Dashboard, chọn **New Item**.
   - Nhập tên dự án (ví dụ: `test-dotnet-pipeline`).
   - Chọn **Pipeline** và nhấn OK.
3. **Cấu hình Pipeline**:
   - Trong phần **Pipeline definition**, chọn **Pipeline script from SCM**.
   - Chọn **SCM** là Git.
4. **Cấu hình Pipeline (Nếu dùng Private Repo)**:
   - Trong phần **Pipeline definition**, chọn **Pipeline script from SCM**.
   - Chọn **SCM** là Git.
   - Nhập repository URL (dạng HTTPS hoặc SSH).
   - Nếu là Private Repo, nhấn **Add** tại mục **Credentials**.
   - **Cách 1: SSH (Khuyên dùng)**:
     - Trên Repo (GitHub/GitLab): Thêm **Deploy Key** (Public Key).
     - Trên Jenkins: Chọn loại `SSH Username with private key` và dán **Private Key** vào.
   - **Cách 2: PAT (Personal Access Token)**:
     - Trên GitHub: Tạo Token (Settings > Developer settings > PAT).
     - Trên Jenkins: Chọn loại `Username with password` (Username là tên GitHub, Password là Token).
   - Để **Script Path** là `Jenkinsfile`.
5. **Lưu và Building**:
   - Nhấn **Build Now** để chạy thử pipeline.

## Hướng dẫn chi tiết Private Repo (GitHub)

### Cách dùng SSH Key (Bảo mật & Ổn định)
1. Chạy `ssh-keygen -t ed25519 -C "jenkins@example.com"` trên máy của bạn (hoặc máy Jenkins).
2. Lấy nội dung file `.pub` dán vào **Settings > Deploy keys** trên Repo GitHub (Check "Allow write access" nếu cần, nhưng chỉ đọc là đủ cho Pipeline).
3. Copy nội dung file Private Key (không có đuôi .pub) vào Jenkins Credentials.

### Cách dùng Token (PAT)
1. Vào GitHub **Settings > Developer settings > Personal access tokens (classic)**.
2. Tạo token mới với quyền `repo` (đủ để clone).
3. Trong Jenkins, thêm Credential loại **Username with password**:
   - Username: `tên_tài_khoản_github`
   - Password: `mã_token_vừa_tạo`

## Các giai đoạn Pipeline
1. **Restore**: Khôi phục các dependencies.
2. **Build**: Biên dịch mã nguồn ở chế độ Release.
3. **Test**: Chạy tất cả các unit tests.
4. **Publish**: Đóng gói ứng dụng vào thư mục `./publish`.
5. **Archive**: Lưu trữ kết quả build làm artifact trong Jenkins.

## Chú ý
Jenkinsfile đã được cấu hình để chạy tương thích trên cả Windows (sử dụng `bat`) và Linux (sử dụng `sh`).
