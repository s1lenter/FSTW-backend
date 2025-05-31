using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Dto;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FSTW_backend.Pdf
{
    public static class PdfCreator
    {
        public static byte[] CreatePdf(AllResumeInfoDto resumeInfo)
        {

            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().Padding(15)
                        .Column(mainColumn =>
                        {
                            mainColumn.Item().Padding(10)
                                .MinHeight(400)
                                .Row(twoColumns =>
                                {
                                    twoColumns.RelativeItem(34).Column(column =>
                                    {
                                        column.Item()
                                            .PaddingBottom(10)
                                            .Text(
                                                $"{resumeInfo.LastName} {resumeInfo.FirstName} {resumeInfo.MiddleName}")
                                            .FontSize(20)
                                            .SemiBold();
                                        column.Item()
                                            .PaddingBottom(5)
                                            .Text("Личная информация")
                                            .FontSize(20)
                                            .SemiBold();
                                        column.Item()
                                            .Text(text =>
                                            {
                                                text.Span("Дата рождения: ").SemiBold();
                                                text.Span(resumeInfo.DateOfBirth.ToString("dd.MM.yyyy"));
                                            });
                                        column.Item()
                                            .PaddingBottom(10)
                                            .Text(text =>
                                            {
                                                text.Span("Пол: ").SemiBold();
                                                text.Span(resumeInfo.Gender);
                                            });

                                        column.Item()
                                            .PaddingBottom(5)
                                            .Text("Контакты")
                                            .FontSize(20)
                                            .SemiBold();

                                        column.Item()
                                            .Text("Номер телефона:")
                                            .SemiBold();
                                        column.Item()
                                            .PaddingBottom(3)
                                            .Text($"{resumeInfo.PhoneNumber}");

                                        column.Item()
                                            .Text("Email:")
                                            .SemiBold();
                                        column.Item()
                                            .PaddingBottom(3)
                                            .Text($"{resumeInfo.Email}");

                                        if (resumeInfo.Telegram != "Не указано" && resumeInfo.Telegram != ""
                                            && resumeInfo.Telegram != "-")
                                        {
                                            column.Item()
                                                .Text("Telegram:")
                                                .SemiBold();
                                            column.Item()
                                                .PaddingBottom(3)
                                                .Text($"{resumeInfo.Telegram}").FontColor(Colors.Blue.Accent4);
                                        }

                                        if (resumeInfo.Vk != "Не указано" && resumeInfo.Vk != "" && resumeInfo.Vk != "-")
                                        {
                                            column.Item()
                                                .Text("VK:")
                                                .SemiBold();
                                            column.Item()
                                                .PaddingBottom(3).Hyperlink($"{resumeInfo.Vk}")
                                                .Text($"{resumeInfo.Vk}").FontColor(Colors.Blue.Accent4);
                                        }

                                        if (resumeInfo.GitHub != "Не указано" && resumeInfo.GitHub != ""
                                            && resumeInfo.GitHub != "-")
                                        {
                                            column.Item()
                                                .Text("GitHub:")
                                                .SemiBold();
                                            column.Item()
                                                .PaddingBottom(3).Hyperlink($"{resumeInfo.GitHub}")
                                                .Text($"{resumeInfo.GitHub}").FontColor(Colors.Blue.Accent4);
                                        }

                                        if (resumeInfo.Linkedin != "Не указано" && resumeInfo.Linkedin != ""
                                            && resumeInfo.Linkedin != "-")
                                        {
                                            column.Item()
                                                .Text("Linkedin:")
                                                .SemiBold();
                                            column.Item()
                                                .PaddingBottom(3).Hyperlink($"{resumeInfo.Linkedin}")
                                                .Text($"{resumeInfo.Linkedin}").FontColor(Colors.Blue.Accent4);
                                        }
                                    });

                                    twoColumns.ConstantItem(1)
                                        .Width(1)
                                        .Background(Colors.Grey.Medium);

                                    twoColumns.RelativeItem(65).PaddingLeft(10).Column(column =>
                                    {
                                        column.Item().PaddingBottom(3).PaddingLeft(15)
                                            .Text("О СЕБЕ").SemiBold();
                                        column.Item().PaddingBottom(3).PaddingLeft(5).LineHorizontal(1)
                                            .LineColor(Colors.Grey.Medium);
                                        column.Item().PaddingLeft(15).Text($"{resumeInfo.About}");
                                        column.Item().PaddingBottom(10).PaddingLeft(15).Text(text =>
                                        {
                                            text.Span("Хобби: ").SemiBold();
                                            text.Span($"{resumeInfo.Hobbies}");
                                        });

                                        column.Item().PaddingBottom(3).PaddingLeft(15)
                                            .Text("ОБРАЗОВАНИЕ").SemiBold();
                                        column.Item().PaddingBottom(3).PaddingLeft(5).LineHorizontal(1)
                                            .LineColor(Colors.Grey.Medium);
                                        foreach (var edu in resumeInfo.Educations)
                                        {
                                            column.Item().PaddingLeft(15).Text(text =>
                                            {
                                                text.Span("Название учбеного заведения: ").SemiBold();
                                                text.Span($"{edu.Place}");
                                            });
                                            column.Item().PaddingLeft(15).Text(text =>
                                            {
                                                text.Span("Специальность: ").SemiBold();
                                                text.Span($"{edu.Specialization}");
                                            });
                                            column.Item().PaddingLeft(15).PaddingBottom(10).Text(text =>
                                            {
                                                text.Span("Год начала/окончания: ").SemiBold();
                                                text.Span(
                                                    $"{edu.StartYear} - {edu.EndYear}");
                                            });
                                        }

                                        column.Item().PaddingBottom(3).PaddingLeft(15)
                                            .Text("НАВЫКИ").SemiBold();
                                        column.Item().PaddingBottom(3).PaddingLeft(5).LineHorizontal(1)
                                            .LineColor(Colors.Grey.Medium);
                                        column.Item().PaddingBottom(10).PaddingLeft(15)
                                            .Text($"{resumeInfo.Skills}");
                                    });

                                    mainColumn.Item().PaddingBottom(3).PaddingLeft(15)
                                        .Text("ОПЫТ").SemiBold();
                                    mainColumn.Item().PaddingBottom(3).PaddingLeft(5).LineHorizontal(1)
                                        .LineColor(Colors.Grey.Medium);
                                    mainColumn.Item().PaddingBottom(10).PaddingLeft(15)
                                        .Text($"{resumeInfo.Experience}");

                                    mainColumn.Item().PaddingBottom(3).PaddingLeft(15)
                                        .Text("ПРОЕКТЫ").SemiBold();
                                    mainColumn.Item().PaddingBottom(3).PaddingLeft(5).LineHorizontal(1)
                                        .LineColor(Colors.Grey.Medium);
                                    foreach (var proj in resumeInfo.Projects)
                                    {
                                        mainColumn.Item().PaddingLeft(15).PaddingBottom(10).Text(text =>
                                        {
                                            text.Span("Название: ").SemiBold();
                                            text.Span($"{proj.Title}");
                                        });
                                        mainColumn.Item().PaddingLeft(15).PaddingBottom(10).Text(text =>
                                        {
                                            text.Span("Описание: ").SemiBold();
                                            text.Span($"{proj.Description}");
                                        });
                                        mainColumn.Item().PaddingLeft(15).PaddingBottom(10).Text(text =>
                                        {
                                            text.Span("Сслыка: ").SemiBold();
                                            text.Hyperlink($"{proj.Link}", $"{proj.Link}")
                                            .FontColor(Colors.Blue.Accent4);
                                        });
                                    }

                                    mainColumn.Item().PaddingBottom(3).PaddingLeft(15)
                                        .Text("ДОСТИЖЕНИЯ").SemiBold();
                                    mainColumn.Item().PaddingBottom(3).PaddingLeft(5).LineHorizontal(1)
                                        .LineColor(Colors.Grey.Medium);
                                    foreach (var ach in resumeInfo.Achievements)
                                    {
                                        mainColumn.Item().PaddingBottom(5).PaddingLeft(15)
                                            .Text($"{ach.Description}");
                                    }
                                });
                        });
                });
            }).GeneratePdf();
        }
    }
}
