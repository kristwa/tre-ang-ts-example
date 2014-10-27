var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var watch = require('gulp-watch');

var config = {
    src : ['ScoreTracker/app/**/*.js', '!ScoreTracker/app/**/*.min.js'],
}

gulp.task('clean', function() {
    del.sync(['ScoreTracker/app/all.min.js']);
});

gulp.task('scripts', ['clean'], function() {
    gulp.src(config.src)
        .pipe(uglify())
        .pipe(concat('all.min.js'))
        .pipe(gulp.dest('ScoreTracker/app/'));
});

gulp.task('watch', function() {
    gulp.watch(config.src, ['scripts']);
});

gulp.task('default', ['scripts'], function() {});