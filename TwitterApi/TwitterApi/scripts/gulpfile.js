"use strict";

var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('compile_sass', function () {
  gulp.src('./scss/**/*.scss')
    .pipe(sass({
      outputStyle: 'compressed'
    }))
    .pipe(gulp.dest('./css'));
});

gulp.task('watch_files', function () {
    gulp.watch('./scss/**/*.scss', ['compile_sass']);
});

gulp.task('default', ['watch_files']);