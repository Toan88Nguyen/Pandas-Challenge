
# Dependencies and Setup.
import pandas as pd

# File to Load (Remember to Change These).
school_data_to_load = (r"C:\Users\Toan Nguyen\Desktop\Pandas-Challenge\PyCitySchools\Resources\schools_complete.csv")
student_data_to_load = (r"C:\Users\Toan Nguyen\Desktop\Pandas-Challenge\PyCitySchools\Resources\students_complete.csv")

# Read School and Student Data File and store into Pandas DataFrames.
school_data = pd.read_csv(school_data_to_load)
student_data = pd.read_csv(student_data_to_load)

# Combine the data into a single dataset.  
school_data_complete = pd.merge(student_data, school_data, how="left", on=["school_name", "school_name"])
school_data_complete.head()

# District Summary
    # Perform the necessary calculations and then create a high-level snapshot of the district's key metrics in a DataFrame.

# Include the following:
    # 1.) Total number of unique schools
    # 2.) Total students.
    # 3.) Total budget.
    # 4.) Average math score.
    # 5.) Average reading score.
    # 6.) % passing math (the percentage of students who passed math).
    # 7.) % passing reading (the percentage of students who passed reading).
    # 8.) % overall passing (the percentage of students who passed math AND reading).

# 1.) Calculate the total number of unique schools.
total_number_schools = len(school_data_complete["school_name"].unique())
total_number_schools

# 2.) Calculate the total number of students.
total_number_students = len(school_data_complete["Student ID"].unique())
total_number_students

# 3.) Calculate the total budget.
total_budget = school_data["budget"].sum()
total_budget

# 4.) Calculate the average (mean) math score.
average_math_score = student_data["math_score"].mean()
average_math_score

# 5.) Calculate the average (mean) reading score.
average_reading_score = student_data["reading_score"].mean()
average_reading_score

# 6.) Calculate the percentage of students with a passing math score (70 or greater).
students_passing_math = school_data_complete.loc[school_data_complete["math_score"] >= 70]
number_students_passing_math = students_passing_math["student_name"].count()
passing_math_percentage = (number_students_passing_math / total_number_students) * 100
passing_math_percentage

# 7.) Calculate the percentage of students who passeed reading (70 or greater).
students_passing_reading = school_data_complete.loc[school_data_complete["reading_score"] >= 70]
number_students_passing_reading = students_passing_reading["student_name"].count()
passing_reading_percentage = (number_students_passing_reading / total_number_students) * 100
passing_reading_percentage

# 8.) Calculate the percentage of the percentage of students who passed math AND reading.
passing_math_reading_count = school_data_complete[
    (school_data_complete["math_score"] >= 70) & (school_data_complete["reading_score"] >= 70)
].count()["student_name"]
overall_passing_rate = passing_math_reading_count /  float(total_number_students) * 100
overall_passing_rate

# Create a high-level snapshot of the district's key metrics in a DataFrame.
district_summary = pd.DataFrame({
    "Total Schools": total_number_schools,
    "Total Students": f"{total_number_students:,}",
    "Total Budget": f"${total_budget:,.2f}",
    "Average Math Score": f"{average_math_score:.6f}",
    "Average Reading Score": f"{average_reading_score:.5f}",
    "% Passing Math": f"{passing_math_percentage:.6f}",
    "% Passing Reading": f"{passing_reading_percentage:.6f}",
    "% Overall Passing": f"{overall_passing_rate: .6f}"
}, index=[0])

district_summary

# School Summary
    # Perform the necessary calculations and then create a DataFrame that summarizes key metrics about each school.

# Include the following:
    # 1.) School name.
    # 2.) School type.
    # 3.) Total students.
    # 4.) Total school budget.
    # 5.) Average math score.
    # 6.) Average reading score.
    # 7.) % passing math (the percentage of students who passed math)
    # 8.) % passing reading (the percentage of students who passed reading)
    # 9.) % overall passing (the percentage of students who passed math AND reading)

# School_data_complete
school_data_complete["passing_math"] = school_data_complete["math_score"] >= 70
school_data_complete["passing_reading"] = school_data_complete["reading_score"] >= 70

school_data_complete

# 1.) Determine the school name.
school_name = school_data_complete.set_index(["school_name"]).groupby(["school_name"])
school_name

# 2.) Determine the school type.
school_types = school_data.set_index(["school_name"])["type"]
school_types

# 3.) Calculate total students
total_student = school_name['Student ID'].count()
total_student

# 4A.) Calculate the total school budget.
per_school_budget = school_data.set_index("school_name")["budget"]
per_school_budget

# 4B.) Calculate per capita spending.
per_school_capita = per_school_budget / total_students
per_school_capita

# 5.) Calculate the Average (mean) math score.
average_math_score = school_name["math_score"].mean() 
average_math_score

# 6.) Calculate the Average (mean) reading score.
average_reading_score = school_name["reading_score"].mean()
average_reading_score

# 7.) Calculate the % passing math (the percentage of students who passed math).
pass_math_percent = school_data_complete[school_data_complete["math_score"] >=70].groupby("school_name")["Student ID"].count()/total_students * 100
pass_math_percent

# 8.) Calculate the % passing reading (the percentage of students who passed reading).
pass_reading_percent = school_data_complete[school_data_complete["reading_score"] >=70].groupby("school_name")["Student ID"].count()/total_students * 100
pass_reading_percent

# 9.) Calculate the % overall passing (the percentage of students who passed math AND reading).
overall_passing_rate = school_data_complete[(school_data_complete['reading_score'] >= 70) & (school_data_complete['math_score'] >= 70)].groupby('school_name')['Student ID'].count()/total_students * 100
overall_passing_rate

# Create a DataFrame called `per_school_summary` with columns for the calculations above.

per_school_summary = pd.DataFrame({
    "School Type": school_types,
    "Total Students": total_student,
    "Per Student Budget": per_school_capita,
    "Total School Budget": per_school_budget,
    "Average Math Score": average_math_score,
    "Average Reading Score": average_reading_score,
    '% Passing Math': pass_math_percent,
    '% Passing Reading': pass_reading_percent,
    "% Overall Passing": overall_passing_rate
})

per_school_summary = per_school_summary[['School Type', 
                          'Total Students', 
                          'Total School Budget', 
                          'Per Student Budget', 
                          'Average Math Score', 
                          'Average Reading Score',
                          '% Passing Math',
                          '% Passing Reading',
                          '% Overall Passing']]


# Formatting
per_school_summary.style.format({'Total Students': '{:}',
                          "Total School Budget": "${:,.2f}",
                          "Per Student Budget": "${:.2f}",
                          'Average Math Score': "{:6f}", 
                          'Average Reading Score': "{:6f}", 
                          "% Passing Math": "{:6f}", 
                          "% Passing Reading": "{:6f}"})


# Highest-Performing Schools (by % Overall Passing)
    # Sort the schools by % Overall Passing in descending order and display the top 5 rows.
    
top_perform = per_school_summary.sort_values("% Overall Passing", ascending = False)
top_perform.head().style.format({'Total Students': '{:}',
                           "Total School Budget": "${:,.2f}", 
                           "Per Student Budget": "${:.2f}", 
                           "% Passing Math": "{:6f}", 
                           "% Passing Reading": "{:6f}", 
                           "% Overall Passing": "{:6f}"})


# Lowest-Performing Schools (by % Overall Passing)
    # Sort the schools by % Overall Passing in ascending order and display the top 5 rows.
    
bottom_five_perform = per_school_summary.sort_values(["% Overall Passing"], ascending=True)
bottom_five_perform.head().style.format({'Total Students': '{:}',
                          "Per Student Budget": "${:,.2f}",
                          "Total School Budget": "${:.2f}",
                          'Average Math Score': "{:6f}", 
                          'Average Reading Score': "{:6f}", 
                          "% Passing Math": "{:6f}", 
                          "% Passing Reading": "{:6f}"})

# Math Scores by Grade.
    # Perform the necessary calculations to create a DataFrame that lists the average math score for students of each grade level (9th, 10th, 11th, 12th) at each school.

nineth_grade = school_data_complete[school_data_complete["grade"] == "9th"].groupby("school_name").mean()["math_score"]
tenth_grade = school_data_complete[school_data_complete["grade"] == "10th"].groupby("school_name").mean()["math_score"]
eleventh_grade = school_data_complete[school_data_complete["grade"] == "11th"].groupby("school_name").mean()["math_score"]
twelveth_grade = school_data_complete[school_data_complete["grade"] == "12th"].groupby("school_name").mean()["math_score"]

#Combine the series into a dataframe
math_grade_dataframe = pd.DataFrame({"Freshman":nineth_grade, "Sophomore":tenth_grade, 
                                     "Junior":eleventh_grade, "Senior":twelveth_grade})
math_grade_dataframe.index.name = "School Name"

# Formatting: Data - cleaner formatting

math_grade_dataframe[["Freshman","Sophomore","Junior","Senior"]] = math_grade_dataframe[["Freshman","Sophomore","Junior","Senior"]].applymap("{:.2f}".format)

#Display

math_grade_dataframe

# Reading Scores by Grade
    # Create a DataFrame that lists the average reading score for students of each grade level (9th, 10th, 11th, 12th) at each school.

nineth_grade = school_data_complete[school_data_complete["grade"] == "9th"].groupby("school_name").mean()["reading_score"]
tenth_grade = school_data_complete[school_data_complete["grade"] == "10th"].groupby("school_name").mean()["reading_score"]
eleventh_grade = school_data_complete[school_data_complete["grade"] == "11th"].groupby("school_name").mean()["reading_score"]
twelveth_grade = school_data_complete[school_data_complete["grade"] == "12th"].groupby("school_name").mean()["reading_score"]

#Combine the series into a dataframe
reading_grade_dataframe = pd.DataFrame({"Freshman":nineth_grade, "Sophomore":tenth_grade, 
                                     "Junior":eleventh_grade, "Senior":twelveth_grade})
reading_grade_dataframe.index.name = "School Name"

# Formatting: Data - cleaner formatting

reading_grade_dataframe[["Freshman","Sophomore","Junior","Senior"]] = reading_grade_dataframe[["Freshman","Sophomore","Junior","Senior"]].applymap("{:.2f}".format)

#Display

reading_grade_dataframe

# Scores by School Spending
    # 1.) Create a table that breaks down school performance based on average spending ranges (per student).
    # 2.) Use the code provided below to create four bins with reasonable cutoff values to group school spending.

# Include the following metrics in the table:
    # 1.) Average math score.
    # 2.) Average reading score.
    # 3.) % passing math (the percentage of students who passed math).
    # 4.) % passing reading (the percentage of students who passed reading).
    # 5.) % overall passing (the percentage of students who passed math AND reading).

# Establish the bins 
spending_bins = [0, 585, 630, 645, 680]
group_names = ["<$585", "$585-630", "$630-645", "$645-680"]

# Create a copy of the school summary since it has the "Per Student Budget" 
school_spending_df = per_school_summary.copy()
school_spending_df

# Categorize spending based on the bins.
per_school_summary["Spending Ranges (Per Student)"] = pd.cut(per_school_capita, spending_bins, labels=group_names)
per_school_summary

# 1.) Calculate the Average math score.
spending_math_scores = per_school_summary.groupby(["Spending Ranges (Per Student)"]).mean()["Average Math Score"]
spending_math_scores

# 2.) Calculate the Average reading score.
spending_reading_scores = per_school_summary.groupby(["Spending Ranges (Per Student)"]).mean()["Average Reading Score"]
spending_reading_scores

# 3.) Calculate the % passing math (the percentage of students who passed math).
spending_passing_math = per_school_summary.groupby(["Spending Ranges (Per Student)"]).mean()["% Passing Math"]
spending_passing_math

# 4.) Calculate % passing reading (the percentage of students who passed reading).
spending_passing_reading = per_school_summary.groupby(["Spending Ranges (Per Student)"]).mean()["% Passing Reading"]
spending_passing_reading

# 5.) Calculate % overall passing (the percentage of students who passed math AND reading).
overall_passing_spending = per_school_summary.groupby(["Spending Ranges (Per Student)"]).mean()["% Overall Passing"]
overall_passing_spending

# Create a Dataframe.
scores_based_spending = pd.DataFrame({"Average Math Score":spending_math_scores,
                                     "Average Reading Score":spending_reading_scores,
                                     "% Passing Math":spending_passing_math,
                                     "%Passing Reading":spending_passing_reading,
                                     "% Overall Passing":overall_passing_spending})


# Display dataframe
scores_based_spending

# Scores by School Size
    # Use pd.cut on the "Total Students" column of the per_school_summary DataFrame.
    # Create a DataFrame called size_summary that breaks down school performance based on school size (small, medium, or large).

# Establish the bins 
size_bins = [0, 1000, 2000, 5000]
group_names = ["Small (<1000)", "Medium (1000-2000)", "Large (2000-5000)"]

# Categorize spending based on the bins.
per_school_summary["School Size"] = pd.cut(per_school_summary["Total Students"], size_bins, labels=group_names)
per_school_summary.head()

# Calculate averages for math score.
size_math_scores = per_school_summary.groupby(["School Size"]).mean()["Average Math Score"]
size_math_scores

# Calculate averages for reading score.
size_reading_scores = per_school_summary.groupby(["School Size"]).mean()["Average Reading Score"]
size_reading_scores

# Calculate passing math.
size_passing_math = per_school_summary.groupby(["School Size"]).mean()["% Passing Math"]
size_passing_math

# Calculate passing reading.
size_passing_reading = per_school_summary.groupby(["School Size"]).mean()["% Passing Reading"]
size_passing_reading

# Calculate overall passing.
size_overall_passing = per_school_summary.groupby(["School Size"]).mean()["% Overall Passing"]
size_overall_passing 

# Create a Dataframe. 
size_summary_df = pd.DataFrame({
          "Average Math Score" : size_math_scores,
          "Average Reading Score": size_reading_scores,
          "% Passing Math": size_passing_math,
          "% Passing Reading": size_passing_reading,
          "% Overall Passing": size_overall_passing})

size_summary_df

# Scores by School Type
    # Use the per_school_summary DataFrame from the previous step to create a new DataFrame called type_summary.
    # This new DataFrame should show school performance based on the "School Type".

# Scores by School Type - Math Scores
type_math_scores = per_school_summary.groupby(["School Type"]).mean()["Average Math Score"]
type_math_scores

# Scores by School Type - Reading Scores
type_reading_scores = per_school_summary.groupby(["School Type"]).mean()["Average Reading Score"]
type_reading_scores

# Scores by School Type - Passing Math
type_passing_math = per_school_summary.groupby(["School Type"]).mean()["% Passing Math"]
type_passing_math

# Scores by School Type - Passing Reading
type_passing_reading = per_school_summary.groupby(["School Type"]).mean()["% Passing Reading"]
type_passing_reading

# Scores by School Type - Overall Passing
type_overall_passing = per_school_summary.groupby(["School Type"]).mean()["% Overall Passing"]
type_overall_passing

# Assemble into DataFrame. 
type_summary_df = pd.DataFrame({
          "Average Math Score" : type_math_scores,
          "Average Reading Score": type_reading_scores,
          "% Passing Math": type_passing_math,
          "% Passing Reading": type_passing_reading,
          "% Overall Passing": type_overall_passing})

type_summary_df

# Written Report
    # 1.) Summarizes the analysis.
        # Base on the data provided, the following trends that I observed while analysing the data for PyCitySchools.
            # 1.) Charter schools outperformed District school with an overall passing percentage of 90.43% vs. 53.67%.
            # 2.) Overall, the data shows that Charter school performed much better than District school across the board in math score, reading score, percentage passing math, and percentage passing reading.
    # 2.) Draws two correct conclusions or comparisons from the calculations.
        # Inconclusion, I believe there are some limiation to this dataset and does not fully give us enought evidence to come with a conclusion of students performances. Needs more data to be use in future studiest. What I would like is information on student-teacher ratio, student socio-economic backround, and/or teacher's educational levels, and even teachers' teaching experiences. With this inforamtion, we would have a better understanding.

